# Streetunes

Streetunes is a web platform where users can create and attend ('follow') street music and street art events. Inspired by Berlin's vibrant street culture, the platform combines elements of graffiti art, music, and urban life to provide an engaging experience for artists and attendees alike.

## Features

- **Event Creation & Discovery:** Users can create, explore, and follow street music and street art events.
- **User Authentication:** Registration and login system to manage user interactions.
- **Profile Page with 3D Avatar:** (Planned) Users can personalize their profiles with a 3D avatar instead of a static image.
- **Trending Events & Featured Artists:** (Future Enhancements) Showcasing popular events and artists.
- **Dynamic UI:** A visually striking homepage featuring graffiti and music-themed elements.
- **Location-Based Event Suggestions:** Uses IPInfo to suggest events near the user’s location based on their public IP.

## Tech Stack

- **Frontend:** HTML, CSS, JavaScript (Possibly enhanced with frontend frameworks in the future)
- **Backend:** ASP.NET MVC (C#)
- **Database:** MS SQL Server with Entity Framework
- **Authentication:** Identity Framework 
- **API Integrations:** IPInfo for location-based event suggestions

## Installation & Setup

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/Streetunes.git
   cd Streetunes
   ```
2. Open the solution in **Visual Studio**.
3. Restore dependencies:
   ```sh
   dotnet restore
   ```
4. Set up the database:
   - Update `appsettings.json` with your database connection string.
   - Run migrations:
     ```sh
     dotnet ef database update
     ```
5. Run the application:
   ```sh
   dotnet run
   ```
6. Open your browser and navigate to `https://localhost:5001/` (or the specified port).

## Contribution

Contributions are welcome! If you have ideas or feature suggestions, feel free to open an issue or submit a pull request.

## License

MIT License. See [LICENSE](LICENSE) for details.

## Contact

For questions or collaboration, feel free to reach out via GitHub Issues.

---

This README provides a solid structure for your GitHub repository. Let me know if you'd like any changes or additions!

