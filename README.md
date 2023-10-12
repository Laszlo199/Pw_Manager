Password Manager mini project
1.	Introduction
 
  We often use passwords for security in many services we use every day. Two-factor authentication isn't always available, so it's important to protect yourself from data breaches by using strong, unique passwords for each service. However, remembering many passwords is hard for most people. That's why password managers are useful. A password manager helps you remember your login details easily. To use it, you need a secure way to 'unlock' it, and then you can see all your saved passwords. It can also help you make strong passwords.
  Goal: The main aim of this project is to make a simple password manager. The password manager should keep your login information safe on one computer or device. It should also be able to create strong passwords. We care a lot about making it safe and easy to use!

2.	Discussion

In this project, we implemented a password manager with a strong focus on security. We used clean architecture, which helps keep our code organized and maintainable and furthermore, it is easy to test and implement security approaches. Our backend employs the Argon2id algorithm with salt and pepper to securely store user passwords. This method ensures that even if our database were compromised, it would be difficult for attackers to recover the original passwords. Additionally, we've incorporated encryption for the stored passwords, making them unreadable to anyone who might gain unauthorized access to our database. We use AES encryption, which adds an extra layer of protection to the sensitive information we store. This ensures the stored passwords and user data in a secure way.


3.	Further suggestions
-	Client-Side Decryption
-	Use the master password in the right way.
-	Implement the possibility of changing the login password
4.	Conclusion

  In general, this project has a good way to learn and practice. We've found some security problems and started fixing them, which makes our system safer and easier to use. To make our password manager even better, we need to keep looking for problems and fixing them. We also want to make sure that if something goes wrong, our system can handle it and stay strong. This will not only improve our system's safety but also make it more user-friendly.

  
5.	How to run
   
    a.	Backend
  
        1. cd Pw_WebApi
        2. dotnet run
       
    If Swagger didn’t automatically open you can open Swagger by using this link: http://localhost:5284/swagger/index.html

b.	Frontend

