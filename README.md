# <h1>Food Wala</h1>

<h2>Food Wala High-Level Architecture</h2>
<h3>1. Frontend:</h3>

Client Application: (Web/Mobile) for customers to browse menus, place orders, and track deliveries.

Restaurant Application: (Web/Mobile) for restaurant staff to manage incoming orders, update menus, and track order status.

<h3>2. Backend Services:</h3>

<h4>API Gateway:</h4>

<b>YARP:</b> Acts as the gateway routing client and restaurant requests to the appropriate microservices.

<h4>Command Services:</h4>

<b>Order Service:</b> Handles order creation, updates, and deletion. Stores data in MS SQL Server.

<b>Customer Service:</b> Manages customer profiles and information. Stores data in MS SQL Server.

<b>Restaurant Service:</b> Manages restaurant profiles, menus, and information. Stores data in MS SQL Server.

<h4>Query Services:</h4>

<b>Order Query Service:</b> Retrieves order data from Redis for fast access.

<b>Customer Query Service:</b> Retrieves customer data from Redis.

<b>Restaurant Query Service:</b> Retrieves restaurant data from Redis.

<h4>Messaging:</h4>

<b>MassTransit with RabbitMQ:</b> Ensures reliable communication between services. Handles events such as order placement, updates, and status changes.

<h3>3. Database:</h3>

<b>MS SQL Server:</b> Stores transactional data for orders, customers, and restaurants.

<b>Redis:</b> Acts as a caching layer for fast query responses and temporary data storage.

<h3>4. Backoffice:</h3>

<b>Admin Portal:</b> Web application for administrators to manage clients (customers and restaurants), monitor system performance, and generate reports.

<h4>Backoffice Services:</h4>

<b>Client Management Service:</b> Manages clients and their data.

<b>Reporting Service:</b> Generates reports on orders, customers, and restaurant performance.

<h3>Data Flow:</h3>

<h4>Client Places Order:</h4>

Client app sends order request to the API Gateway.

API Gateway routes the request to the Order Service.

Order Service stores order data in MS SQL Server and sends an event to RabbitMQ.

RabbitMQ forwards the event to the necessary services (e.g., Restaurant Service, Notification Service).

<h4>Order Processing:</h4>

Restaurant Service updates the restaurant with new order details.

Restaurant staff updates order status via the restaurant app.

Status updates are sent through RabbitMQ to the Order Service.

Order Service updates order status in MS SQL Server and caches the new status in Redis.

<h4>Querying Data:</h4>

Client app requests order status from the API Gateway.

API Gateway routes the request to the Order Query Service.

Order Query Service retrieves the data from Redis and sends it back to the client app.

<h3>Benefits:</h3>
Scalability: Decoupled services with MassTransit and RabbitMQ enable easy scaling.

Performance: Using Redis for queries ensures fast data retrieval.

Flexibility: YARP allows easy routing and service management.

<h3>Technology Stack:</h3>
<b>MS SQL Server:</b> For command storage.

<b>Redis:</b> For query caching.

<b>MassTransit:</b> For messaging.

<b>RabbitMQ:</b> As the message broker.

<b>YARP:</b> For API Gateway.
