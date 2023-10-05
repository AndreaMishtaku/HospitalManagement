import AuthManager from "../../../utils/authManager";
import { AppThunk } from "../../redux/appThunk";
import { navigateTo } from "../navigation/navigation.store";
import { setUser } from "./user.store";

const onLogin =
  (payload: any): AppThunk =>
  async (dispatch) => {
    try {
      const response = await AuthManager.loginWithCredentials({ ...payload });
      if (response.user && response.token) {
        dispatch(setUser(response.user));
        dispatch(navigateTo(`/${response.user.role.toLowerCase()}`));
      }
    } catch (err: any) {
      Error(err.message);
    }
  };

export default onLogin;
