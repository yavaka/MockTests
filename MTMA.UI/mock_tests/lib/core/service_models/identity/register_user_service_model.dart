class RegisterUserServiceModel {
  String username;
  String email;
  String password;
  String confirmPassword;

  RegisterUserServiceModel({
    required this.username,
    required this.email,
    required this.password,
    required this.confirmPassword,
  });

  Map<String, String> get toJson => {
        "Username": username,
        "Email": email,
        "Password": password,
        "ConfirmPassword": confirmPassword,
      };
}
