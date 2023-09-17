import { Client, cacheExchange, fetchExchange } from "urql";

export const client = new Client({
  url: "https://localhost:7000/graphql",
  exchanges: [cacheExchange, fetchExchange],
});
