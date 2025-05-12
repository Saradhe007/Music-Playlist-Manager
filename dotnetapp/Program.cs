using System;
using System.Data.SqlClient;
using dotnetapp.Models;

namespace dotnetapp
{
    public class Program
    {
        public static string ConnectionString { get; } = 
@"Server=(localdb)\MSSQLLocalDB;Database=appdb;Trusted_Connection=True;Encrypt=False;";

        public static void AddSong(Song song)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Songs (Title, Artist, Album, Genre, Duration, PlayCount) VALUES (@Title, @Artist, @Album, @Genre, @Duration, @PlayCount)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", song.Title);
                    cmd.Parameters.AddWithValue("@Artist", song.Artist);
                    cmd.Parameters.AddWithValue("@Album", song.Album);
                    cmd.Parameters.AddWithValue("@Genre", song.Genre);
                    cmd.Parameters.AddWithValue("@Duration", song.Duration);
                    cmd.Parameters.AddWithValue("@PlayCount", song.PlayCount);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Song added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void ViewAllSongs()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Songs", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No songs found.");
                        return;
                    }
                    while (reader.Read())
                    {
                        Console.WriteLine($"SongID: {reader["SongID"]}, Title: {reader["Title"]}, Artist: {reader["Artist"]}, Album: {reader["Album"]}, Genre: {reader["Genre"]}, Duration: {reader["Duration"]}, Play Count: {reader["PlayCount"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void SearchSongByTitle(string title)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Songs WHERE Title = @Title", conn);
                    cmd.Parameters.AddWithValue("@Title", title);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        Console.WriteLine($"No song found with Title: {title}.");
                        return;
                    }
                    while (reader.Read())
                    {
                        Console.WriteLine($"SongID: {reader["SongID"]}, Title: {reader["Title"]}, Artist: {reader["Artist"]}, Album: {reader["Album"]}, Genre: {reader["Genre"]}, Duration: {reader["Duration"]}, Play Count: {reader["PlayCount"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void FilterSongsByGenre(string genre)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Songs WHERE Genre = @Genre", conn);
                    cmd.Parameters.AddWithValue("@Genre", genre);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        Console.WriteLine($"No songs found with Genre: {genre}.");
                        return;
                    }
                    while (reader.Read())
                    {
                        Console.WriteLine($"SongID: {reader["SongID"]}, Title: {reader["Title"]}, Artist: {reader["Artist"]}, Album: {reader["Album"]}, Genre: {reader["Genre"]}, Duration: {reader["Duration"]}, Play Count: {reader["PlayCount"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void UpdateSongById(Song updatedSong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Songs SET Title = @Title, Artist = @Artist, Album = @Album, Genre = @Genre, Duration = @Duration, PlayCount = @PlayCount WHERE SongID = @SongID", conn);
                    cmd.Parameters.AddWithValue("@Title", updatedSong.Title);
                    cmd.Parameters.AddWithValue("@Artist", updatedSong.Artist);
                    cmd.Parameters.AddWithValue("@Album", updatedSong.Album);
                    cmd.Parameters.AddWithValue("@Genre", updatedSong.Genre);
                    cmd.Parameters.AddWithValue("@Duration", updatedSong.Duration);
                    cmd.Parameters.AddWithValue("@PlayCount", updatedSong.PlayCount);
                    cmd.Parameters.AddWithValue("@SongID", updatedSong.SongID);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine($"Song with ID {updatedSong.SongID} updated successfully.");
                    else
                        Console.WriteLine($"No song found with ID {updatedSong.SongID}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void DeleteSong(string title, string artist)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Songs WHERE Title = @Title AND Artist = @Artist", conn);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Artist", artist);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine($"Song with Title {title} deleted successfully.");
                    else
                        Console.WriteLine($"No song found with Title & Artist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nMusic Playlist Manager Menu - Enter your choice (1-7):");
                Console.WriteLine("1. Add Song");
                Console.WriteLine("2. View All Songs");
                Console.WriteLine("3. Search Song by Title");
                Console.WriteLine("4. Filter Songs by Genre");
                Console.WriteLine("5. Update Song by ID");
                Console.WriteLine("6. Delete Song by Title and Artist");
                Console.WriteLine("7. Exit");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Song newSong = new Song();
                        Console.Write("Enter Title: ");
                        newSong.Title = Console.ReadLine();
                        Console.Write("Enter Artist: ");
                        newSong.Artist = Console.ReadLine();
                        Console.Write("Enter Album: ");
                        newSong.Album = Console.ReadLine();
                        Console.Write("Enter Genre: ");
                        newSong.Genre = Console.ReadLine();
                        Console.Write("Enter Duration (in seconds): ");
                        newSong.Duration = int.Parse(Console.ReadLine());
                        Console.Write("Enter Play Count: ");
                        newSong.PlayCount = int.Parse(Console.ReadLine());
                        AddSong(newSong);
                        break;

                    case "2":
                        ViewAllSongs();
                        break;

                    case "3":
                        Console.Write("Enter Title to Search: ");
                        SearchSongByTitle(Console.ReadLine());
                        break;

                    case "4":
                        Console.Write("Enter Genre to Filter: ");
                        FilterSongsByGenre(Console.ReadLine());
                        break;

                    case "5":
                        Song updatedSong = new Song();
                        Console.Write("Enter Song ID to Update: ");
                        updatedSong.SongID = int.Parse(Console.ReadLine());
                        Console.Write("Enter Title: ");
                        updatedSong.Title = Console.ReadLine();
                        Console.Write("Enter Artist: ");
                        updatedSong.Artist = Console.ReadLine();
                        Console.Write("Enter Album: ");
                        updatedSong.Album = Console.ReadLine();
                        Console.Write("Enter Genre: ");
                        updatedSong.Genre = Console.ReadLine();
                        Console.Write("Enter Duration (in seconds): ");
                        updatedSong.Duration = int.Parse(Console.ReadLine());
                        Console.Write("Enter Play Count: ");
                        updatedSong.PlayCount = int.Parse(Console.ReadLine());
                        UpdateSongById(updatedSong);
                        break;

                    case "6":
                        Console.Write("Enter Title to Delete: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter Artist: ");
                        string artist = Console.ReadLine();
                        DeleteSong(title, artist);
                        break;

                    case "7":
                        Console.WriteLine("Exiting the application...");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
