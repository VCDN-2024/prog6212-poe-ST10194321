# Contract Monthly Claim System (CMCS)

The Contract Monthly Claim System (CMCS) is an ASP.NET Core MVC application designed to streamline the management of monthly claims for lecturers and staff in an academic institution. It offers a secure and efficient way to submit, review, and process claims while generating invoices for approved submissions.

---
### Homepage
![Homepage](imagesREAD/Screenshot%202024-11-21%20193523.png)

## Features

### Lecturer Features
- **Submit Claims:** Lecturers can submit claims with details like hours worked, modules, and supporting documents.
![Submit Claim](imagesREAD/Screenshot%202024-11-21%20194310.png)
- **Claim Status Tracking:** View submitted claims and track their status (Pending, Approved, Rejected).
![Claims Overview](imagesREAD/Screenshot%202024-11-21%20201022.png)

### HR Features
- **Lecturer Management:** Update lecturer details such as name, email, and contact information.
![HR Dashboard](imagesREAD/Screenshot%202024-11-21%20201208.png)
- **Invoice Generation:** Automatically generate detailed invoices for approved claims, including totals across multiple claims.
![Invoice Summary](imagesREAD/Screenshot%202024-11-22%20114545.png)

### Program Coordinator & Academic Manager Features
- **Claim Review:** Approve or reject claims submitted by lecturers.
- **Dashboard Overview:** View all claims and their statuses
![Review Claims](imagesREAD/Screenshot%202024-11-21%20201123.png)

### Key Functionalities
- **Secure Authentication:** Role-based access using Azure Identity ensures only authorized users can access relevant pages.
- **File Upload:** Supporting documents can be uploaded with size limits and file type restrictions (PDF, DOCX, TXT).
- **Dynamic Invoice Generation:** Generate downloadable HTML invoices styled to match the application’s theme.

---

## Technologies Used

- **Backend Framework:** ASP.NET Core MVC
- **Database:** SQL Server
- **Authentication:** Azure Identity
- **Frontend:** Razor Views, HTML5, CSS3
- **Styling Frameworks:** Custom CSS and Google Fonts (Bakbak One, SF Pro)
- **Session Management:** ASP.NET Core Sessions

---

## Database Schema

### Tables:
1. **Users:**
   - Stores user details such as name, email, role, and hashed passwords.

2. **Lecturers:**
   - Stores lecturer-specific details (e.g., name, contact).

3. **Claims:**
   - Manages claim submissions, including hours, modules, and uploaded documents.

4. **Invoices:**
   - Tracks invoices generated for lecturers, linking claims and totals.

---

## Installation

### Prerequisites
- .NET 6 or later
- SQL Server
- Visual Studio 2022 or Visual Studio Code

### Steps to Run Locally
1. Clone the repository:
   ```bash
   git clone <repository_url>
   cd <repository_folder>
