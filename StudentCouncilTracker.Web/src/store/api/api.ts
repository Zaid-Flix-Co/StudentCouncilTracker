import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query"

const API_URL = "https://localhost:7090/api"

export const api = createApi({
    reducerPath: "api",
    tagTypes: [
        "CatalogUser"
    ],
    baseQuery: fetchBaseQuery({
        baseUrl: API_URL
    }),
    endpoints: () => ({}),
})

export const {} = api