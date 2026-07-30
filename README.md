#  BulkyBook — E-Commerce Bookstore Platform

A full-stack e-commerce web application for a bulk-order bookstore, built with **ASP.NET Core MVC** and **Entity Framework Core**. The platform supports public book browsing with tiered volume pricing, role-based authentication, and a complete admin management dashboard for products, categories, and orders.



---

## ✨ Features

### Customer-Facing Storefront
- Browse a categorized book catalog with a polished, responsive grid layout
- Detailed product pages showing volume-based pricing tiers (1–50, 51–100, 100+ units)
- Rich-text product descriptions (rendered via Quill editor content)
- Secure account registration and login (ASP.NET Core Identity)
- Custom user profile fields (name, phone, street address, city, state, postal code)

### Admin Dashboard
- Dedicated admin layout with a persistent sidebar and mobile-friendly offcanvas navigation
- Full CRUD management for **Categories** and **Products**
- Server-side DataTables integration for fast search, sort, and pagination
- Rich-text product description editor (Quill)
- Product image upload with automatic file handling and preview
- Role-based access control (`Admin`, `Employee`, `Customer`) enforced at the controller level
- Dark mode support across all admin and customer views

### Authentication & Authorization
- ASP.NET Core Identity with a custom `ApplicationUser` model
- Role-based registration and route protection (`[Authorize(Roles = ...)]`)
- Return-URL redirect flow after login/register
- Client- and server-side validation on all forms

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 10) |
| ORM | Entity Framework Core 10 |
| Database | Microsoft SQL Server |
| Auth | ASP.NET Core Identity |
| Frontend | Bootstrap 5, Bootstrap Icons |
| Data Tables | DataTables.js |
| Rich Text | Quill.js |
| Notifications | Toastr, SweetAlert2 |
| Architecture | Layered / N-Tier (Models → DataAccess → Business → Web) |

---

## 🏗️ Architecture

The solution follows a clean, layered architecture, separating concerns across dedicated class libraries:

```
BulkyBook.Models          → Domain entities & ViewModels
BulkyBook.DataAccess       → EF Core DbContext, migrations, data access
BulkyBook.Business          → Service layer (business logic, interfaces)
BulkyBook.Utility           → Shared constants (roles, static definitions)
BulkyBookWeb                → ASP.NET Core MVC web application (Areas: Admin, Customer, Identity)
```

This separation keeps data access, business rules, and presentation logic independently testable and maintainable — a structure that scales cleanly as new features are added.

---

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB, Express, or full instance)
- Visual Studio 2026 (recommended) or VS Code

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/ghgasimovhikmat/E-Commerce-Bookstore.git
   cd E-Commerce-Bookstore
   ```

2. **Configure your connection string**

   Add an `appsettings.json` file under `BulkyBookWeb/` (this file is git-ignored and not committed for security):
   ```json
   {
     "ConnectionStrings": {
       "SQLConnection": "Server=localhost;Database=BulkyBook;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   cd BulkyBook.DataAccess
   dotnet ef database update --startup-project ../BulkyBookWeb
   ```

4. **Run the application**
   ```bash
   cd ../BulkyBookWeb
   dotnet run
   ```

5. Navigate to `https://localhost:{port}` in your browser.

---

## 👤 Roles

The application seeds three roles on first registration:

| Role | Access |
|---|---|
| `Admin` | Full access to the admin dashboard, product/category management |
| `Employee` | Elevated access for internal staff operations |
| `Customer` | Standard storefront access, default role for public sign-ups |

---

## 🗺️ Roadmap

- [ ] Shopping cart & checkout flow
- [ ] Payment integration
- [ ] Order history & tracking for customers
- [ ] Deploy to Azure App Service + Azure SQL Database
- [ ] Automated testing (unit + integration)

---

## 📄 License

This project is available for educational and portfolio purposes.

---

## 👨‍💻 Author

**Built by [Hikmat Gasimov]**
