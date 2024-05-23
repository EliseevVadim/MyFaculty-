# Contributing to MyFaculty

Thank you for considering contributing to our project! This document outlines the steps to help you make a contribution to the MyFaculty project.

## Introduction

MyFaculty consists of a backend app built with ASP.NET Core with ideas of [Clean Architecture](https://www.ezzylearning.net/tutorial/building-asp-net-core-apps-with-clean-architecture), an identification server built using Identity Server4 (ASP.NET MVC project) and a frontend built with Vue.js, mostly using [Vuetify 2](https://v2.vuetifyjs.com/) components library. We welcome any contributions, including bug reports, improvement suggestions, new features, code fixes, tests, and documentation.

## How You Can Help

- Reporting bugs
- Suggesting new features
- Improving documentation
- Fixing bugs
- Writing new tests
- Optimizing existing code

## Contributions Wishlist

Here are some areas where we would love to see contributions:

- **Backend project**:
  - Adding new API endpoints
  - Optimizing existing ones
  - Enchancing the performance
- **Identity server**:
  - Adding new OAuth authentication methods for external platforms
- **Frontend project**:
  - Creating new components/logics according to API updates
  - Improving UI/UX of the platform (but staying close to [Material Design](https://m2.material.io) concepts)
  - Increasing adaptivity of UI to mobile phones
- **Bug Fixes**
  - Reviewing issues reported in the issue tracker
  - Fixing reported bugs
- **Testing**
  - Writing unit tests
  - Writing integration tests
  - Writing end-to-end tests for both backend and frontend

Feel free to pick any task from the above wishlist or suggest your own ideas!

## Development Process

1. [**Fork**](https://github.com/EliseevVadim/MyFaculty/fork) this repository.
2. Create a new branch:
    ```bash
    git checkout -b feature/my-new-feature
    ```
3. Make your changes.
4. Commit your changes:
    ```bash
    git commit -m '<type of change>: Add some feature'
    ```
    Please, consider using the [Semantic commit messages](https://gist.github.com/joshbuchea/6f47e86d2510bce28f8e7f42ae84c716) to make your change more understandable and clear.
5. Push to the branch:
    ```bash
    git push origin feature/my-new-feature
    ```
6. Create a pull request on GitHub.

## Code Style

### C#

- Use 4 spaces for indentation.
- Follow the [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions).

### Vue.js

- Use 2 spaces for indentation.
- Follow the official [Vue Style Guide](https://vuejs.org/v2/style-guide/).

## Creating Pull Requests

- Ensure your code passes all tests.
- Follow the coding standards outlined above.
- Pull request descriptions should be clear and detailed.
- Include a description of the problem your PR solves and how you solved it.

## Contact Information

If you have any questions, please open an issue in this repository or contact me through email [eliseevv02@mail.ru](mailto:eliseevv02@mail.ru) or [Telegram](https://t.me/VadimEliseev02).

Thank you for your contribution!
