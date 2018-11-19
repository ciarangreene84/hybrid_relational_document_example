# Hybrid Relational/Document Database Example

The purpose of this repo is to provide an example of a hybrid relational/document database which was sketched as a proof of concept/experiment. This example uses C# and MS SQL 2016.

## Setup

1. Clone/download this repo.
2. Create the database by either:
    * Open the solution in Visual Studio and publish the database project to a HRDE database on your localhost.
    * Create a HRDE on your localhost and execute the ./Hrde.Database/dbo/Tables/Accounts.sql script.
3. Open the solution in Visual Studio and run all unit tests.

### Problem

We have a system which is deployed in multiple different territories. There is *universal* data structures/constraints/rules that hold true for all territories. There are also *specific* data structures/constraints/rules that apply only in specific territories. 

The problem is that the database must be strict enough to enforce the *universal* data structures/constraints/rules but also flexible enough to enable rapid re-use.

### Solution

Relational databases can provide strict rules, but with the downside that they are not very flexible. In order to add a new property, for example, one must update the tables/views/functions, data access layer, etc.

Document database exist on the other end of the spectrum. They are highly flexible and easier to change, but ensuring the integrity of the data is more difficult than in relational databases.

This example solution to problem is to create a hybrid relational/document database. Those properties which are *universal* can be stored in a relational way and anything which is *specific* can be stored in a document.

![Relational/Document split example](Example.png)

### Downsides

Such a hybrid is never going to be as fast as either a purely relational or document database. Understanding how entites are packed and unpacked from the database, while not overly complex, requires care and attention. 
 
## Packing/Unpacking

Key to the hybrid database solution is how the entities are packed/unpacked. 

There will be two representations of each entity. One represents the entity as it will be stored in the database and one represents the entity as it will be used in code.

![Object Document Container Explaination](ObjectDocumentContainerExplaination.png)

In the above example, we have an Account entity. In the data access layer (and database), we have an AccountId, Type, Name, ObjectDocument and ObjectHash. The AccountId, Type and Name are *universal* properties, meaning that *all* deployments of the system will have Accounts with those properties.

We also have two types of Account; a CustomerAccount and a CorporateAccount. Both of these inherit from the base Repository.Account and therefore have the universal properties. However the CustomerAccount has a DateOfBirth, Nationality and Sex, whereas the CorporateAccount has a BusinessNumber. The properties of the descendent classes are stored in the ObjectDocument.

## IObjectDocumentSerializer

The IObjectDocumentSerializer is responsible for packing and unpacking any non-universal properties. Note that the non-universal properties are serialized as JSON and that the universal properties are *not* serialized.

```C#
 public interface IObjectDocumentSerializer
 {
     T2 Deserialize<T1, T2>(T1 objectDocumentContainer) where T1 : ObjectDocumentContainer;
     IEnumerable<T2> Deserialize<T1, T2>(IEnumerable<T1> objectDocumentContainers) where T1 : ObjectDocumentContainer;

     T2 Serialize<T1, T2>(T1 repositoryObject) where T2 : ObjectDocumentContainer;
 }
```

The Hrde.RepositoryLayer.Tests.Integration.Serialization.ObjectDocumentSerializerTests has a number of tests which demonstrate the de/serialization of the entities. Debug through these tests until the mechanism is understood.

