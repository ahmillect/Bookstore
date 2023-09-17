import AuthorList from "@/components/Authors/AuthorList";
import Head from "next/head";
import { Inter } from "next/font/google";
import styles from "@/styles/Home.module.css";

const inter = Inter({ subsets: ["latin"] });

function AuthorsPage() {
  return (
    <>
      <Head>
        <title>Authors</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <div className={`${styles.main} ${inter.className}`}>
        <h1>Authors</h1>
        <AuthorList />
      </div>
    </>
  );
}

export default AuthorsPage;
