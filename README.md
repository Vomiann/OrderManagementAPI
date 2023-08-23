------------------------------------------
How to Configure and Run the Application:
------------------------------------------

Download the project (OrderManagementAPI) from the GitHub repository and build it.
Install MS SQL database on your machine if not already installed.
Configure the connection to your database:
3.1 Open the appsettings.json file of your project.
3.2 Locate the ConnectionStrings section.
3.3 Replace the value of DefaultConnection with your own connection string for the database.
Next, you need to create the project database and apply migration to generate the necessary tables: Customer and Orders:

Open the command prompt (or terminal) in your project directory or use the Package Manager Console in Visual Studio.
Execute the command to create the database based on migrations:
Enter the command:
dotnet ef database update --startup-project OrderManagementAPI (here we specify the main Web API project)

After this, the database and all necessary tables (Customer and Orders) should be created.

After creating the database and tables, you need to configure Azure Service Bus:
4.1 Go to the Azure Portal and create a Service Bus service if not already created.
4.2 Obtain the connection string value for your Service Bus service.
4.3 Insert this value into the corresponding section of your appsettings.json (AzureServiceBusConnection).
4.4 Next, create a queue in Azure Service Bus:
In the Azure Portal, find your Service Bus service.
In the "Shared access policies" section, create a new access policy.
In the "Queues" section, create a new queue and assign the created access policy to it.
In appsettings.json, set the QueueName that you created in the Azure Portal for the queue.
In this solution (OrderManagementAPI), there is also a project named ConsumerApp. Go there and in the Program.cs class, add the _serviceBusConnectionString and _queueName according to your Azure Service Bus settings.

Finally, when all configurations are done, you need to start 2 projects:
ConsumerApp (console application) and OrderManagementAPI (Web API Core).
The Web API Core is integrated with Swagger, which allows making requests to the controllers of the OrderManagementAPI application. To start, you need to create a Customer, and then an Order. Specify the CustomerId for which the order will be created.

Other controller methods allow fetching Customer and Order data. Additionally, a controller method is implemented: PATCH /orders/{orderId} for updating the order status.

In the current implementation of order statuses, there are two parameters: 0 (New - order created but not paid) and 1 (Paid - order paid). This is a simple implementation for a test project. It is assumed that a table of order statuses will be created later, describing statuses in both numeric and textual forms, with a one-to-many relationship to the Order table. Currently, this is just a test variant.

After a successfully created order, the running console application ConsumerApp will read the Azure Service Bus message queue and display in the console how many orders have arrived. The counter shows the number of incoming orders.
In my educational free account, only queue creation is possible; I couldn't create a Topic because I lack the permission. The implementation for a Topic would be similar to that of a queue. The only difference is that a Topic can work with more listeners than a Queue.


---------------------------------------------------
Brief Overview of Accepted Architectural Decisions:
---------------------------------------------------

The OrderManagementAPI assembly has an OrderManagementAPI layer for working with controllers and request/response data models. The OrderManagementDAL layer contains the business logic implementation (services that work with the database). In this layer, OrderManagementDAL, service models and database models are also separated.
Mappers exist for each of the OrderManagementAPI and OrderManagementDAL layers to map models between them. This is done to avoid a direct connection between database models and service models. Thus, we encapsulate the connection between the database and controller logic.

According to SOLID principles, all abstractions are defined as interfaces (in this case, service implementations).

There's also the OrderManagementDALTests project for unit tests. In this case, unit tests are implemented for the Order service (OrderServiceTests). Database interaction is taken care of using an in-memory context. We don't physically create a database; instead, we perform checks on data in memory.