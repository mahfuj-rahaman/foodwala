# foodwala

<h1>FoodWala High-Level Architecture</h1>
<h3>1. Frontend:</h3>

Client Application: (Web/Mobile) for customers to browse menus, place orders, and track deliveries.

Restaurant Application: (Web/Mobile) for restaurant staff to manage incoming orders, update menus, and track order status.

2. Backend Services:

API Gateway:

YARP: Acts as the gateway routing client and restaurant requests to the appropriate microservices.

Command Services:

Order Service: Handles order creation, updates, and deletion. Stores data in MS SQL Server.

Customer Service: Manages customer profiles and information. Stores data in MS SQL Server.

Restaurant Service: Manages restaurant profiles, menus, and information. Stores data in MS SQL Server.

Query Services:

Order Query Service: Retrieves order data from Redis for fast access.

Customer Query Service: Retrieves customer data from Redis.

Restaurant Query Service: Retrieves restaurant data from Redis.

Messaging:

MassTransit with RabbitMQ: Ensures reliable communication between services. Handles events such as order placement, updates, and status changes.

3. Database:

MS SQL Server: Stores transactional data for orders, customers, and restaurants.

Redis: Acts as a caching layer for fast query responses and temporary data storage.

4. Backoffice:

Admin Portal: Web application for administrators to manage clients (customers and restaurants), monitor system performance, and generate reports.

Backoffice Services:

Client Management Service: Manages clients and their data.

Reporting Service: Generates reports on orders, customers, and restaurant performance.

Data Flow:
Client Places Order:

Client app sends order request to the API Gateway.

API Gateway routes the request to the Order Service.

Order Service stores order data in MS SQL Server and sends an event to RabbitMQ.

RabbitMQ forwards the event to the necessary services (e.g., Restaurant Service, Notification Service).

Order Processing:

Restaurant Service updates the restaurant with new order details.

Restaurant staff updates order status via the restaurant app.

Status updates are sent through RabbitMQ to the Order Service.

Order Service updates order status in MS SQL Server and caches the new status in Redis.

Querying Data:

Client app requests order status from the API Gateway.

API Gateway routes the request to the Order Query Service.

Order Query Service retrieves the data from Redis and sends it back to the client app.

Benefits:
Scalability: Decoupled services with MassTransit and RabbitMQ enable easy scaling.

Performance: Using Redis for queries ensures fast data retrieval.

Flexibility: YARP allows easy routing and service management.

Technology Stack:
MS SQL Server: For command storage.

Redis: For query caching.

MassTransit: For messaging.

RabbitMQ: As the message broker.

YARP: For API Gateway.
