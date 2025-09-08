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
<img width="1868" height="742" alt="image" src="https://github.com/user-attachments/assets/1d67899e-391c-45c6-994e-687ad6012d52" />
<img width="1895" height="895" alt="image" src="https://github.com/user-attachments/assets/9cf02f4f-ea7e-4081-8bc6-32fe7e8ef06b" />
<img width="1745" height="861" alt="image" src="https://github.com/user-attachments/assets/ab1eec56-abd5-4296-b8ba-c7a8fddec817" />
<img width="1895" height="895" alt="image" src="https://github.com/user-attachments/assets/cee344b6-f9e8-44ee-bdc4-2009a2040fc0" />
<img width="1788" height="877" alt="image" src="https://github.com/user-attachments/assets/917316d8-f328-4414-a95e-65c553c18b79" />
<img width="1851" height="863" alt="image" src="https://github.com/user-attachments/assets/b4034800-3fb7-404f-9cf2-b72f00bc58fa" />



---

## 🚀 Getting Started  

1. Clone the repository  
   ```bash
   git clone https://github.com/your-username/your-repo.git
