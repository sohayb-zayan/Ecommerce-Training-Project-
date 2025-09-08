# Ecommerce-Training-Project-
one of my step to learn Asp.net 

# 🛒 E-Commerce Training Project (ASP.NET MVC)

## 📌 Overview  
This project is a **training application** built with **ASP.NET MVC**.  
The main focus is on implementing **Admin Panel**, **Authentication & Authorization**, and applying **Clean Architecture concepts**.  

---

## 🔹 Features  

### 👤 Authentication & Authorization  
- Implemented using **ASP.NET Identity**.  
- Role-based access control (**Admin / User**).  
- Customized **Identity Options**:  
  - Password policy (length, special chars, upper/lowercase).  
  - Unique email required.  
  - Custom login settings.  
- Customized **Authentication Cookies**:  
  - Expiration time (10 minutes).  
  - Sliding expiration enabled.  
  - Custom LoginPath & AccessDeniedPath.  

### 🛠️ Admin Panel Features  
- Full **CRUD operations** for:  
  - **Products** (with image upload/edit/delete).  
  - **Categories**.  
- **DataTables** integration for category management.  
- **Select2** integration to display products based on category.  
- **SweetAlert** for confirmation dialogs (edit & delete).  
- **Toastr Notifications** for feedback (e.g., after creation).  

### 🏗️ Architecture & Infrastructure  
- Project structured with **Areas** (Admin, Customer, Identity).  
- Separate **Data Access Layer** and **Model Layer**.  
- **Unit of Work Pattern** implemented for better DB transaction management.  
- **Seed Service** to initialize roles & users.  
- **Redirect Service** for navigation handling.  
- Middleware to handle root path (`/`) redirection based on user role:  
  - Admin → `/Admin/Category/Index`  
  - User → `/Customer/Home/Index`  
  - Unauthorized → Login/AccessDenied  
- **Status Code Pages handling**:  
  - `401 Unauthorized` → Redirect to Login.  
  - `403 Forbidden` → Redirect to AccessDenied.  

---

## 💡 What I Practiced & Learned  
- Building secure authentication & authorization flows.  
- Handling file upload & management in ASP.NET MVC.  
- Enhancing UX with **SweetAlert, Toastr, Select2, DataTables**.  
- Strengthening database operations using **EF Core**.  
- Applying **Clean Architecture principles** to structure the project.  
- Developing a functional **Admin Dashboard**.  

---

## 🛠️ Tech Stack  
- **ASP.NET MVC**  
- **C#**  
- **Entity Framework Core**  
- **SQL Server**  
- **Ajax**  
- **SweetAlert**  
- **Toastr.js**  
- **DataTables**  
- **Select2**  

---

## 📸 Screenshots  
<img width="1880" height="745" alt="image" src="https://github.com/user-attachments/assets/bf645cb4-bac8-4792-b98c-bd550bb0f383" />
<img width="1862" height="867" alt="image" src="https://github.com/user-attachments/assets/9dc60aaa-1807-415f-8fa1-87fc5ca34de3" />
<img width="1620" height="757" alt="image" src="https://github.com/user-attachments/assets/d46a1d77-baff-408f-855c-34e1e7b954bd" />
<img width="1067" height="656" alt="image" src="https://github.com/user-attachments/assets/3644928f-73f2-456e-bffb-acf73c7bfcc8" />

<img width="1340" height="705" alt="image" src="https://github.com/user-attachments/assets/cf592d17-df36-47ce-9015-b862e2f02a0b" />
<img width="833" height="836" alt="image" src="https://github.com/user-attachments/assets/76935e13-f99d-40c6-867a-03fa398c7999" />

![Uploading image.png…]()


---

## 🚀 Getting Started  

1. Clone the repository  
   ```bash
   git clone https://github.com/your-username/your-repo.git
