ShipIt Inventory Management
===========================

Copyright 2007.

# Setup Instructions

## Running the application

### With Visual Studio

To run the app via Visual Studio:
* Open the `ShipIt.sln` solution by going to `File` -> `Open` -> `Project/Solution`
* Add a connections.config to both the ShipIt and ShipItTest projects, adding a connection string to each e.g.
```
<connectionStrings>
  <add name="MyPostgres" providerName="System.Data.SqlClient" connectionString="Server=127.0.0.1;Port=5432;Database=ShipItTest;User Id=postgres; Password=password;" />
</connectionStrings>
```
  * Ensure that the main and test connection strings point to different databases

### On AWS Elastic Beanstalk

<!-- To update a running [AWS Elastic Beanstalk](https://aws.amazon.com/elasticbeanstalk/) instance:

* Update the version number in `pom.xml`.
* Build via `mvn install`, note the name of the war which was just built.
* From the AWS console, go to `Services` -> `Elastic Beanstalk`, and choose your instance
  from the dashboard.   Choose `Upload and Deploy`, then `Browse...` and choose the war
  which you just build with `mvn install`.  Click `Deploy`.  Wait for the deployment to
  be marked as successful.

To check the logs:  From the AWS console, go to `Services` -> `Elastic Beanstalk`, and
choose your instance from the dashboard.   Click `Logs` on the left, then `Request Logs`.

In the unlikely event that you need to change any of the injected configuration, for
example the database connection string or password, then these are available under
`Configuration` - `Software Configuration`.

Information on the CPU utilisation, and network utilisation is available under `Monitoring`,
it may also be interesting to look at the utilisation or logs of the MySQL database instance
which backs this application.  These are available under `Services` -> `RDS` -> `Instances`
- `shipit`. -->

## Unit Tests

Run the tests in Visual Studio by right clicking on the `ShipItTest` project and
choosing "Run Tests".

Due to the "age" of the codebase, the unit tests rely on a connection to the database.
Therefore you will need to ensure that you have a database setup whose schema matches
the production database.  This database can be local or remote.  The details of this
database are set in the test project's connections.config file that you should have created earlier.
