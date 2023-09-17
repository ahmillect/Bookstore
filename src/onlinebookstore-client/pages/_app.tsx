import "@/styles/globals.css";
import { client } from "@/utils/urqlClient";
import { Provider } from "urql";
import type { AppProps } from "next/app";

function App({ Component, pageProps }: AppProps) {
  return (
    <Provider value={client}>
      <Component {...pageProps} />
    </Provider>
  );
}

export default App;
