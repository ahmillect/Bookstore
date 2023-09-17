import Head from "next/head";
import BookList from "../components/Books/BookList";
import { Inter } from "next/font/google";
import styles from "@/styles/Home.module.css";

const inter = Inter({ subsets: ["latin"] });

function BooksPage() {
  return (
    <>
      <Head>
        <title>Books</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <div className={`${styles.main} ${inter.className}`}>
        <h1>Books</h1>
        <BookList />
      </div>
    </>
  );
}

export default BooksPage;
