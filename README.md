# AIRecommender

AIRecommender is an AI-powered book recommendation system developed in C#. It uses collaborative filtering techniques to suggest books based on user preferences and ratings.

## Features

- Load and process book, user, and rating data from CSV files
- Calculate similarities between books using Pearson correlation
- Aggregate ratings based on user preferences
- Generate personalized book recommendations

## Getting Started

### Prerequisites

- .NET Core SDK (version 3.1 or later)
- Visual Studio 2019 or later (optional, for development)

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/AIRecommender.git
   ```

2. Navigate to the project directory:
   ```
   cd AIRecommender
   ```

3. Build the project:
   ```
   dotnet build
   ```

### Usage

1. Prepare your CSV data files (books.csv, ratings.csv, users.csv) and place them in a known directory.

2. Update the file paths in `Program.cs`:
   ```csharp
   string booksPath = @"C:\Path\To\Your\books.csv";
   string ratingsPath = @"C:\Path\To\Your\ratings.csv";
   string usersPath = @"C:\Path\To\Your\users.csv";
   ```

3. Run the program:
   ```
   dotnet run
   ```

4. The program will generate book recommendations based on the sample preference in `Program.cs`. Modify the `Preference` object to test different scenarios.

## Project Structure

- `AIRecommender.Core/`: Contains the core logic of the recommendation system
  - `Entities/`: Data models (Book, User, BookUserRating, etc.)
  - `Interfaces/`: Interfaces for the main components
  - `Implementations/`: Concrete implementations of the interfaces
- `AIRecommender.Tests/`: Contains unit tests for the project

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
