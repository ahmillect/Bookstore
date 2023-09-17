import Head from "next/head";
import { Inter } from "next/font/google";
import styles from "@/styles/Home.module.css";
import EditProfile from "@/components/User/EditProfile";

const inter = Inter({ subsets: ["latin"] });

function UpdateUserPage() {
  return (
    <>
      <Head>
        <title>Update User</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <div className={`${styles.main} ${inter.className}`}>
        <h1>Please Update your info</h1>
        <EditProfile />
      </div>
    </>
  );
}

export default UpdateUserPage;
