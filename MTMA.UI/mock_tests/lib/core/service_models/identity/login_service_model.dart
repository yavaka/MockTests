class LoginServiceModel {
  String email;
  String password;

  LoginServiceModel({
    required this.email,
    required this.password,
  });

  Map<String, String> get toJson => {
        "Email": email,
        "Password": password,
      };
}
