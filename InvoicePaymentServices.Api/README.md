# InvoicePaymentServices
Invoice payment services

User Story:
As a user, I want to be able to process payments for vendor/supplier Invoices. To achieve this, I need a web app that allows me to select a Invoice to pay, specify payment details, save the payment information, and view a list of payment information for paid Invoices.
 
Task Acceptance Criteria:
1.	The system should provide a list of Invoices, and I should be able to select one Invoice to create payments for.
2.	When creating a payment, I should be able to enter the following information: amount, debit date, and payment method (e.g., Bank transfer, Email transfer, Credit card).
3.	After entering the payment information, I should be able to submit it.
4.	Multiple payments can be attributed to a single Invoice until it is completely settled.
5.	Once a payment has been created, the system should automatically mark the Invoice as paid if the full amount has been paid off.
6.	The system should maintain a list of paid Invoices, including their payment information.
7.	The web app should handle any errors or exceptions and provide clear error messages when necessary.
 
Task Component Requirements:
•	The solution can be implemented with framework(s) of your choice.
•	The solution requires to have a backend, as well as a GUI.
•	The backend needs to have a way to persist the data. This resource should not just be a static List but an in-memory resource, SQLite or file system is perfectly acceptable as well.
•	The solution should include appropriate tests.
 
Wireframes:
•	Home page
 ![image](https://github.com/MingGitForPlooto/InvoicePaymentServices/assets/147672072/381a73a6-c83b-4a4c-9425-c42a87aad3cb)

•	Create payment
 ![image](https://github.com/MingGitForPlooto/InvoicePaymentServices/assets/147672072/833c55be-d693-444d-85f1-ed36c47c3e69)

•	Payment history
 ![image](https://github.com/MingGitForPlooto/InvoicePaymentServices/assets/147672072/f5190648-573e-4747-acd8-f38b13268919)

Evaluation Criteria:
Your solution will be scored based on the following criteria:
•	User experience: your solution should be easy for users to understand, use and navigate. It doesn’t need to be pretty.
•	Functionality: your solution should meet all of the requirements outlined above.
•	Code quality: your code should follow best practices, including industry standards like persistent storage, tests, logging and error handling.
•	Setup & Deployment: your solution should have instructions for building, running, and deploying the code.
 
Things that will make our life easier:
•	A README file that tells about the solution structure and environment requirements (runtime version, node version etc. as applicable)
•	Please document any assumptions you make in the README file.
•	Please don’t include compiled binaries in your GitHub repository.
 

