# Distribution of MyFaculty

Thank you for considering using our project for your university! This document outlines the steps to help you do this in the best way possible.

## Installation

### Prerequisites

- .NET Core SDK 3.1 or later
- Node.js and npm
- MySQL Server (or any other compatible SQL database)

### Backend Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/EliseevVadim/MyFaculty.git
   cd MyFaculty
   ```
2. Restore NuGet packages for each subproject:
   ```bash
   cd MyFaculty.Backend/MyFaculty.<Subproject>
   dotnet restore
   ```
3. Update the database connection string, domains info, and admin email credentials in MyFaculty.WebApi/appsettings.json.
4. Apply database migrations:
   ```bash
   cd MyFaculty.Backend/MyFaculty.Persistence
   dotnet ef database update
   ```
5. Run the WebApi project:
   ```bash
   cd MyFaculty.Backend/MyFaculty.WebApi
   dotnet run
   ```
### Frontend Setup

1. Navigate to the frontend directory:
    ```bash
    cd my_faculty.frontend
    ```
2. Install npm dependencies:
    ```bash
    npm install
    ```
3. Run the frontend project:
    ```bash
    npm run serve
    ```

## Usage

- Open your browser and navigate to http://localhost:3000 to access the frontend.
- The backend API can be accessed at http://localhost:44317 or the domain specified inside `MyFaculty.WebApi/appsettings.json`.
- The identity server can be accessed at http://localhost:44395 or the domain specified inside `MyFaculty.WebApi/appsettings.json`.

## Project Naming Convention

To maintain consistency and clearly identify cloned versions of the MyFaculty project, we recommend naming your project in the following format: `MyFaculty <University Name>`. This naming convention helps to easily distinguish different deployments of the platform across various universities. Examples include:

- MyFaculty MSU
- MyFaculty ITMO
- MyFaculty MIPT

Using this convention ensures that each instance of the MyFaculty project remains recognizable and distinct, facilitating better communication and collaboration among different universities using the platform.

## Contact Information

If you have any questions, please open an issue in this repository or contact me through email [eliseevv02@mail.ru](mailto:eliseevv02@mail.ru) or [Telegram](https://t.me/VadimEliseev02).

Thank you for your interest in our project!
