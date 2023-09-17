import Head from "next/head";
import { Inter } from "next/font/google";
import styles from "@/styles/Home.module.css";
import Login from "@/components/User/Login";

const inter = Inter({ subsets: ["latin"] });

function LoginPage() {
  return (
    <>
      <Head>
        <title>Login</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <div className={`${styles.main} ${inter.className}`}>
        <h1>Please Log In</h1>
        <Login />
      </div>
    </>
  );
}

export default LoginPage;
