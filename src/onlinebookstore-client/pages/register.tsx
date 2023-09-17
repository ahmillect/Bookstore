import Head from "next/head";
import { Inter } from "next/font/google";
import styles from "@/styles/Home.module.css";
import Register from "@/components/User/Register";

const inter = Inter({ subsets: ["latin"] });

function RegisterPage() {
  return (
    <>
      <Head>
        <title>Register</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <div className={`${styles.main} ${inter.className}`}>
        <h1>Please Sign Up</h1>
        <Register />
      </div>
    </>
  );
}

export default RegisterPage;
