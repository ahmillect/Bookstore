import { Client, cacheExchange, fetchExchange } from "urql";

export const client = new Client({
  url: "https://localhost:7000/graphql",
  exchanges: [cacheExchange, fetchExchange],
  fetchOptions: () => {
    const token = localStorage.getItem("token");
    return {
      headers: { authorization: token ? `Bearer ${token}` : "" },
    };
  },
});
