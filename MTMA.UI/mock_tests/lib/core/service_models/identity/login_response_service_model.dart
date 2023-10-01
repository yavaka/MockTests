import 'dart:convert';

class LoginResponseServiceModel {
  late bool succeeded;
  late String? error;
  late String? token;

  LoginResponseServiceModel({required this.succeeded, this.token, this.error});

  factory LoginResponseServiceModel.fromJson(dynamic json) {
    Map<String, dynamic> data = jsonDecode(json);

    // errors
    if (data.containsKey('errors')) {
      String error = '';

      for (var err in data['errors']) {
        error = err['value'];
      }

      return LoginResponseServiceModel(
        succeeded: false,
        error: error,
      );
    }

    // token
    return LoginResponseServiceModel(succeeded: true, token: data['token']);
  }
}
