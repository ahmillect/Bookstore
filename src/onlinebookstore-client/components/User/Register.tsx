import { useState } from "react";
import { useMutation } from "urql";
import { Button, TextField, Grid } from "@mui/material";
import router from "next/router";

const REGISTER_USER_MUTATION = `
  mutation RegisterUser($input: RegisterUserInput!) {
    registerUser(input: $input) {
      user {
        username
      }
    }
  }
`;

function Register() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");
  const [result, executeMutation] = useMutation(REGISTER_USER_MUTATION);

  const handleRegister = async (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();

    const userInput = {
      username,
      password,
      email,
    };

    const response = await executeMutation({ input: { input: userInput } });

    if (response.data && response.data.registerUser) {
      router.push("/success-signup");
    }
  };

  return (
    <Grid container direction="column" alignItems="center" spacing={2}>
      <Grid item>
        <TextField
          label="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </Grid>
      <Grid item>
        <TextField
          type="password"
          label="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </Grid>
      <Grid item>
        <TextField
          label="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </Grid>
      <Grid item>
        <Button variant="contained" color="primary" onClick={handleRegister}>
          Register
        </Button>
      </Grid>
    </Grid>
  );
}

export default Register;
