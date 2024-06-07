export const path = {
  HOME: "/*",
  LOGIN: "login",
  REGISTER: "register",
  PROFILE: "profile",
  BOOKS: "books",
  DETAILBOOKS__ID: "books/:id",
  EDITBOOKS__ID: "system/books/:id/edit",
  CREATEBOOK: "system/books/create",
  BORROWEDBOOKS: "books/borrowed",
  DASHBOARD: "system",
  CATEGORIES: "system/categories",
  MANAGEBOOKS: "system/books",
  MANAGEREQUESTS: "system/requests",
  EDITCATEGORY__ID: "system/categories/:id/edit",
  DETAILCATEGORY__ID: "/categories/:id",
  CREATECATEGORY: "system/categories/create",
  DETAILREQUEST__ID: "system/requests/:id",
};


export const sidebarmenu = [
  {
    id: 1,
    text: "Dashboard",
    path: "/",
    color:
      "w-full bg-[#2C3841] text-[#FFFFFF] font-Oswald border-b border-white p-2 hover:bg-gradient-to-r from-purple-400 via-pink-500 to-red-500",
  },
  {
    id: 2,
    text: "Manage Categories",
    path: "/system/categories",
    color:
      "w-full bg-[#2C3841] text-[#FFFFFF] font-Oswald border-b border-white p-2 hover:bg-gradient-to-r from-purple-400 via-pink-500 to-red-500",
  },
  {
    id: 3,
    text: "Manage Books",
    path: "/system/books",
    color:
      "w-full bg-[#2C3841] text-[#FFFFFF] font-Oswald border-b border-white p-2 hover:bg-gradient-to-r from-purple-400 via-pink-500 to-red-500",
  },
  {
    id: 4,
    text: "Manage Requests",
    path: "/system/requests",
    color:
      "w-full bg-[#2C3841] text-[#FFFFFF] font-Oswald border-b border-white p-2 hover:bg-gradient-to-r from-purple-400 via-pink-500 to-red-500",
  },
  {
    id: 5,
    text: "Manage Users",
    path: "/system/users",
    color:
      "w-full bg-[#2C3841] text-[#FFFFFF] font-Oswald border-b border-white p-2 hover:bg-gradient-to-r from-purple-400 via-pink-500 to-red-500",
  },
];
