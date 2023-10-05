import JwtManager from "./jwtManager";

import axios from "axios";
import { navigateTo } from "../store/stores/navigation/navigation.store";

export interface IUserInfo {
  user: any;
  token: string;
}

class AuthManager {
  static get token(): string | null {
    return JwtManager.accessToken;
  }

  public static parseJwt(token: string) {
    if (!token) {
      return;
    }
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace("-", "+").replace("_", "/");
    return JSON.parse(window.atob(base64));
  }

  static async getTokenWithCredentials(payload: any): Promise<IUserInfo> {
    const { data } = await axios.post("authentication/login", payload);
    const user = await AuthManager.parseJwt(data.accessToken);

    const responseLogin: IUserInfo = {
      user: user,
      token: data?.accessToken,
    };

    if (responseLogin?.token) {
      JwtManager.setAccessToken(responseLogin.token);
    }

    return responseLogin;
  }

  static async loginWithCredentials(credentials: any): Promise<IUserInfo> {
    const response = await AuthManager.getTokenWithCredentials(credentials);
    return response;
  }

  static async register(user: any): Promise<void> {
    const res = await axios.post("authentication/register", user);
    if (res.data) {
      JwtManager.setAccessToken(res.data.token);
      window.location.pathname = "/";
    }
  }
  static logout() {
    JwtManager.clearToken();
  }
}

export default AuthManager;
