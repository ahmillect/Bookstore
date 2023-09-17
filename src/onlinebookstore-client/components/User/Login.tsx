import { useState } from "react";
import { useClient } from "urql";
import { Button, TextField, Grid, Typography } from "@mui/material";
import router from "next/router";

const LOGIN_QUERY = `
  query Login($username: String!, $password: String!) {
    login(input: { username: $username, password: $password })
  }
`;

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const client = useClient();

  const handleLogin = async (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    const response = await client
      .query(LOGIN_QUERY, { username, password })
      .toPromise();
    if (response.data && response.data.login) {
      localStorage.setItem("token", response.data.login);
      router.push("/success-login");
    }
  };

  return (
    <Grid container direction="column" alignItems="center" spacing={2}>
      <Grid item>
        <Typography variant="h4">Login</Typography>
      </Grid>
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
        <Button variant="contained" color="primary" onClick={handleLogin}>
          Login
        </Button>
      </Grid>
    </Grid>
  );
}

export default Login;
