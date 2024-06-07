import { useRoutes } from "react-router-dom";
import React, { Suspense } from "react";
import { RequiredAuth, SuperAuth } from "../components";
import { path } from "./routeContants";

const LoginLazy = React.lazy(() => import("../pages/Login"));
const RegisterLazy = React.lazy(() => import("../pages/Register"));

const HomeLazy = React.lazy(() => import("../pages/Home"));
const BooksLazy = React.lazy(() =>
  import("../pages/Normal/BorrowingBooks/Books")
);
const CreateBookLazy = React.lazy(() => import("../pages/Super/Books/CreateBooks"));
const DetailBookLazy = React.lazy(() => import("../pages/DetailBook"));
const EditBookLazy = React.lazy(() => import("../pages/Super/Books/EditBook"));
const BorrowedBooksLazy = React.lazy(() =>
  import("../pages/Normal/BorrowedBooks/BorrowedBooks")
);
const CategoriesAdminLazy = React.lazy(() =>
  import("../pages/Super/Categories/Categories")
);
const RequestAdminLazy = React.lazy(() =>
  import("../pages/Super/BorrowingRequests/BorrowingRequests")
);
const BooksAdminLazy = React.lazy(() => import("../pages/Super/Books/Books"));
const EditCategoryLazy = React.lazy(() => import("../pages/Super/Categories/EditCategories"));
const CreateCategoryLazy = React.lazy(() => import("../pages/Super/Categories/CreateCategories"));
const DetailRequestLazy = React.lazy(() => import("../pages/Super/BorrowingRequests/DetailBorrowingRequests"))
const DetailCategoriesLazy = React.lazy(() => import("../pages/Super/Categories/DetailCategories"))


export const AppRoute = () => {
  const elements = useRoutes([
    {
      path: path.HOME,
      element: (
        <Suspense>
          <HomeLazy />
        </Suspense>
      ),
    },
    {
      path: path.BOOKS,
      element: (
        <Suspense>
          <BooksLazy />
        </Suspense>
      ),
    },
    {
      path: path.DETAILBOOKS__ID,
      element: (
        <Suspense>
          <DetailBookLazy />
        </Suspense>
      ),
    },
    {
      path: path.LOGIN,
      element: (
        <Suspense>
          <LoginLazy />
        </Suspense>
      ),
    },
    {
      path: path.REGISTER,
      element: (
        <Suspense>
          <RegisterLazy />
        </Suspense>
      ),
    },
    {
      path: path.BORROWEDBOOKS,
      element: (
        <RequiredAuth>
          <Suspense>
            <BorrowedBooksLazy />
          </Suspense>
        </RequiredAuth>
      ),
    },
    {
      path: path.CATEGORIES,
      element: (
        <RequiredAuth>
          <SuperAuth>
            <Suspense>
              <CategoriesAdminLazy />
            </Suspense>
          </SuperAuth>
        </RequiredAuth>
      ),
    },
    {
      path: path.MANAGEREQUESTS,
      element: (
        <RequiredAuth>
          <SuperAuth>
            <Suspense>
              <RequestAdminLazy />
            </Suspense>
          </SuperAuth>
        </RequiredAuth>
      ),
    },
    {
      path: path.MANAGEBOOKS,
      element: (
        <RequiredAuth>
          <SuperAuth>
            <Suspense>
              <BooksAdminLazy />
            </Suspense>
          </SuperAuth>
        </RequiredAuth>
      ),
    },
    {
      path: path.CREATEBOOK,
      element: (
        <RequiredAuth>
          <SuperAuth>
            <Suspense>
              <CreateBookLazy />
            </Suspense>
          </SuperAuth>
        </RequiredAuth>
      ),
    },
    {
      path: path.EDITBOOKS__ID,
      element: (
        <RequiredAuth>
          <SuperAuth>
            <Suspense>
              <EditBookLazy />
            </Suspense>
          </SuperAuth>
        </RequiredAuth>
      ),
    },
    {
      path: path.EDITCATEGORY__ID,
      element: (
        <RequiredAuth>
          <SuperAuth>
            <Suspense>
              <EditCategoryLazy />
            </Suspense>
          </SuperAuth>
        </RequiredAuth>
      ),
    },
    {
      path: path.CREATECATEGORY,
      element: (
        <RequiredAuth>
          <SuperAuth>
            <Suspense>
              <CreateCategoryLazy />
            </Suspense>
          </SuperAuth>
        </RequiredAuth>
      ),
    },
    {
      path: path.DETAILREQUEST__ID,
      element: (
        <RequiredAuth>
          <SuperAuth>
            <Suspense>
              <DetailRequestLazy />
            </Suspense>
          </SuperAuth>
        </RequiredAuth>
      ),
    },
    {
      path: path.DETAILCATEGORY__ID,
      element: (
        <RequiredAuth>
          <SuperAuth>
            <Suspense>
              <DetailCategoriesLazy />
            </Suspense>
          </SuperAuth>
        </RequiredAuth>
      ),
    },
  ]);

  return elements;
};
