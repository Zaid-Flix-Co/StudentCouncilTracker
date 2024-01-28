import {
  ICatalogUserData,
  ICatalogUserDto,
} from "../../../types/catalog-user/catalog-user.types";
import { IOperationResult } from "../../../types/operation-result/operation-result.types";
import { api } from "../api";

const URL = "CatalogUser";

export const catalogUserApi = api.injectEndpoints({
  endpoints: (builder) => ({
    getCatalogUsers: builder.query<IOperationResult<ICatalogUserDto>, string>({
      query: (searchTerm) => `/${URL}/?q=${searchTerm}`,
      providesTags: (result, error, searchTerm) => [
        {
          type: "CatalogUser",
          id: searchTerm,
        },
      ],
    }),
    getCatalogUser: builder.query<IOperationResult<ICatalogUserDto>, number>({
      query: (id) => ({
        url: `/${URL}/Get/${id}`,
        method: "POST",
      }),
      providesTags: (result, error, id) => [
        {
          type: "CatalogUser",
          id: id,
        },
      ],
    }),
    createCatalogUsers: builder.mutation<
      IOperationResult<ICatalogUserDto>,
      ICatalogUserData
    >({
      query: (user) => ({
        body: user,
        url: `/${URL}/create`,
        method: "POST",
      }),
      invalidatesTags: () => [
        {
          type: "CatalogUser",
        },
      ],
    }),
    updateCatalogUsers: builder.mutation<
      IOperationResult<ICatalogUserData>,
      { id: number; user: ICatalogUserData }
    >({
      query: ({ id, user }) => ({
        body: user,
        url: `/${URL}/${id}`,
        method: "PUT",
      }),
      invalidatesTags: () => [
        {
          type: "CatalogUser",
        },
      ],
    }),
    deleteCatalogUsers: builder.mutation<null, number>({
      query: (id) => ({
        url: `/${URL}/${id}`,
        method: "Delete",
      }),
      invalidatesTags: () => [
        {
          type: "CatalogUser",
        },
      ],
    }),
  }),
});

export const {
  useCreateCatalogUsersMutation,
  useDeleteCatalogUsersMutation,
  useUpdateCatalogUsersMutation,
  useGetCatalogUserQuery,
  useGetCatalogUsersQuery,
} = catalogUserApi;
