<h3>Custom MVC Authentication in ASP.NET Core</h3>
    This project is a custom implementation of an authentication and authorization system using ASP.NET Core MVC. The solution demonstrates how to build a       complete user authentication flow from scratch without relying on built-in frameworks like ASP.NET Core Identity. It includes user registration,             login, logout, and access control based on roles or user claims.

<h4>Key Features</h4>
    User Registration and Login: Custom user registration and login with form validation, using IActionResult methods to handle GET and POST requests.
    Custom Authentication: Implementation of authentication using cookies with ASP.NET Core's CookieAuthenticationDefaults.AuthenticationScheme.
    User Roles and Claims: Manage user roles and claims to control access to different areas of the application.
    Password Hashing: Secure password storage using hashing (e.g., BCrypt) to enhance security.
    Custom Authorization Logic: Custom authorization filters to protect routes and actions based on user roles or claims.
    Persistent Login: Support for persistent user sessions using cookies, with options for "Remember Me" functionality.
    Error Handling: Validation errors and feedback messages to guide users during registration and login.
    MVC Pattern: Clean separation of concerns following the MVC (Model-View-Controller) architecture pattern.
    Project Structure
    Controllers: Contains the AccountController and LoginController, which handle user authentication flows.
    Models: Includes the UserAccount and RegistrationModel classes, defining the user data structure and validation logic.
    Views: Razor views for the registration, login, and other UI components that interact with users.
    Data Access: Uses Entity Framework Core for database interactions with a custom DbContext (AppDbContext) to manage user data.
    Services: May include custom services for handling authentication, authorization, and user management.
<h4>How It Works</h4>
    User Registration: Users register by providing a username, email, password, and other required information. Input is validated on the server side.
    Login Process: After a successful login, users are authenticated using cookie-based authentication. Claims are created to store user information securely.
    Authorization: Routes and actions are protected by checking user roles or claims, ensuring only authorized users can access certain parts of the application.
    Security: Passwords are securely hashed before storage, and proper validation ensures data integrity and security against common vulnerabilities.
<h4>Technologies Used</h4>
    ASP.NET Core MVC
    Entity Framework Core
    Cookie Authentication
    Razor Views
    C#
<h4>Future Improvements</h4>
    Add Two-Factor Authentication (2FA) for additional security.
    Integrate OAuth or OpenID Connect for third-party authentication.
    Implement account recovery features, such as password reset and account lockout.
    Contributions
    Contributions, suggestions, and feedback are welcome! Please feel free to open an issue or submit a pull request.

<h4>License</h4>
    This project is licensed under the MIT License - see the LICENSE file for details.


<h4>Getting Started</h4>
1. Clone the Repository:

            git clone https://github.com/yourusername/your-repository-name.git
            cd your-repository-name
2. Install Dependencies
3. Configure Database:
4. Run the Application:
