# Blogg

This is a web-application for an online blogging platform built using ASP.NET for the backend API
and React.js with Bootstrap for the frontend.

## Building the Project

#### Build Dependencies

The following tools are required to build the project:

- **.NET Core SDK**

    Latest dotnet SDK version is required. The project was initially made with `dotnet-sdk-7.0.x`
    but has later been retargeted to use `dotnet-sdk-8.0.x`.

- **Node.JS**

    Any recent version of Node.JS should work. However the project was built using
    `Node.js v20.11.0`.

- **MySQL**

    `MySQL 8.0.x` was used to create the project.

---

### Api

1. Restore Project
    - In the project root directory, run the command
    ```
    dotnet restore
    ```
    This command should download all the project's library dependencies, and allow it to be built.

2. Setup database

    - Install EF Tools. This will help commit database migrations to the database.
        ```
        dotnet tool install --global dotnet-ef
        ```

    - Update the database. This command should be run once you are in the `Api` project directory.
        ```
        dotnet ef database update
        ```
        This command should commit the database changes / migrations to the database and intialize
        the DB.

3. Build & Run the Api server

    - To build & Run the backend, run the following command:
    ```
    dotnet run
    ```
    This will compile the project and start up the project's backend server.

### Client

1. Install project dependencies
    ```
    npm install
    ```
    This will install all the project library dependencies.

2. Start the client server
    ```
    npm run dev
    ```
    This will start the project's frontend server. This should start a server on
    `http://localhost:5173/`.


Ensure that both the client and api servers are running. If you visit
`http://localhost:5268/swagger/index.html` in your browser of choice, you should see the Swagger
Documentation of the project's Api, and visiting `http://localhost:5173/` should show you frontend
of the project.
