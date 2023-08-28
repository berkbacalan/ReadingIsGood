# ReadingIsGood
# Project Documentation

This documentation provides detailed information about the **ReadingIsGood** project. ReadingIsGood is a project that forms the basis of an online book sales platform, allowing for the management of book orders.

## Table of Contents

1. [Project Description](#project-description)
2. [Requirements](#requirements)
3. [Installation](#installation)
4. [Usage](#usage)
5. [API Documentation](#api-documentation)
6. [Database Structure](#database-structure)
7. [Troubleshooting](#troubleshooting)

## Project Description

**ReadingIsGood** is an online book sales platform where users can browse books, place orders, and track order statuses. Users can register and place orders for their desired books.

## Requirements

- Docker
- .NET Core SDK 7.0
- MSSQL Server

## Installation

1. Navigate to the project root (ReadingIsGood) directory and execute the following commands in order to start the project:
````
docker-compose up -d
````

2. After the Docker containers for the database and web application are up and running, open your browser and go to [http://localhost/swagger/index.html](http://localhost/swagger/index.html) to access the application.

## Usage

1. Open your browser and navigate to [http://localhost/swagger/index.html](http://localhost/swagger/index.html).
2. Create a book with `api/v1/Book/Create` endpoint.
3. Create a customer with `api/v1/Customer/Create` endpoint.
4. Create orders by adding books which you already created to orderList via `api/v1/Order/Create`.
5. Monitor your orders via Get requests.

**Note:** You may find the `ReadingIsGood API.postman_collection.json` file to import postman collection.

## API Documentation

The project's API documentation is accessible via Swagger UI. Follow these steps to access the API documentation:

1. Open your browser and navigate to [http://localhost/swagger/index.html].
2. View all API endpoints and parameters.

## Database Structure

The database is managed within an MSSQL database named `ReadingIsGoodDb`. The relevant tables and relationships include:

- Users
- Books
- Orders

## Troubleshooting

If you encounter any issues with the project, you can try the following steps:

1. Execute the following commands in the project directory to restart the containers:

````
docker-compose down
docker-compose up -d
````


2. If the problem persists, review application log files and Docker container logs for more information about the error.
