Password Manager mini project
1. Introduction
 
  We often use passwords for security in many services we use every day. Two-factor authentication isn't always available, so it's important to protect yourself from data breaches by using strong, unique passwords for each service. However, remembering many passwords is hard for most people. That's why password managers are useful. A password manager helps you remember your login details easily. To use it, you need a secure way to 'unlock' it, and then you can see all your saved passwords. It can also help you make strong passwords.
  Goal: The main aim of this project is to make a simple password manager. The password manager should keep your login information safe on one computer or device. It should also be able to create strong passwords. We care a lot about making it safe and easy to use!

2. Discussion

In this project, we implemented a password manager with client-side encryption and JWT authentication.
We used clean architecture, which helps keep our code organized and maintainable and furthermore, it is easy to test and implement security approaches. 

For the Client implementation, we used Blazor WASM. This means that the website runs on the client computer. 
This has both pros and cons:
+ [+] the master password is never stored or transmitted to our servers

- [-] running in a web browser means slower performance when performing cryptographic functions (In our case we had to reduce the amount of iterations to prevent the login page from being too slow)


On login, the client's master password is hashed (PBKDF2<SHA256>) with their email 4000 times - this results in a **Master Key** > Used to decrypt the vault

The Master Key is then hashed(PBKDF2<SHA256>) another 1000 times to create the **Master Hash** > Used in place of a password for logging into the API 

When logging in, the API further hashes the Master Hash using Argon2id with a random hash and pepper to safely store it in the database

When the user is logged in, their master key is only stored within memory. This mean that if they lose connection or close 
their browser, the memory is cleared and the master key is no longer on the machine (user will have to re-login)

When saving a new password, both the email and the password are encrypted using the Master Key before being saved in the database


3.	Further suggestions
-	Stretching the master key to 512 bits
-	Add the ability to update the User's master password
-	Improve the registration functionality
-   Fix some validation error on the FE (they don't affect current functionality, but they are still errors)
-   Find a different way to store emails to avoid **Known Plaintext Attacks**

4.	Conclusion

  In general, this project has a good way to learn and practice. We've found some security problems and started fixing them, which makes our system safer and easier to use. To make our password manager even better, we need to keep looking for problems and fixing them. We also want to make sure that if some exceptions are thrown by the code, the system can handle them. This will not only improve our system's safety but also make it more user-friendly.

  
5.	How to run
   
    a. Backend
  
        1. cd Pw_WebApi
        2. dotnet run
   
    a. Frontend
  
        1. cd PW_Frontend
        2. dotnet run
       

6. Group:

   Laszlo Tolnay
   Tomáš Peniak

