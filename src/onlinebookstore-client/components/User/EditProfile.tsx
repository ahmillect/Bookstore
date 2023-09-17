import router from "next/router";
import { useState } from "react";
import { useClient } from "urql";

const UPDATE_USER_MUTATION = `
  mutation UpdateUser($input: UpdateUserInput!) {
    updateUser(input: $input) {
      user {
        username
        email
      }
    }
  }
`;

function EditProfile() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const client = useClient();

  const handleUpdate = async (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();

    const updateInput = {
      id: "65058ada585bfebf2a259c86",
      email,
      password,
    };

    const response = await client
      .mutation(UPDATE_USER_MUTATION, { input: { input: updateInput } })
      .toPromise();
    if (response.data) {
      router.push("/");
    }
  };

  return (
    <div>
      <h2>Edit Profile</h2>
      <form>
        <label>
          Username:
          <input
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </label>
        <label>
          Email:
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </label>
        <label>
          Password:
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </label>
        <button onClick={handleUpdate}>Update</button>
      </form>
    </div>
  );
}

export default EditProfile;
