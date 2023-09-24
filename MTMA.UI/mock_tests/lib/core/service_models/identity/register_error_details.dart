import 'dart:convert';

class RegisterErrorDetailsServiceModel {
  bool succeeded;
  List<String>? usernameErrors;
  List<String>? emailErrors;
  List<String>? passwordErrors;
  List<String>? confirmPasswordErrors;

  RegisterErrorDetailsServiceModel({this.succeeded = false, this.usernameErrors, this.emailErrors, this.passwordErrors, this.confirmPasswordErrors});

  factory RegisterErrorDetailsServiceModel.fromJson(dynamic json) {
    Map<String, dynamic> data = jsonDecode(json);

    // data contains title only if fluent validations does not pass
    if (data.containsKey('title')) {
      var errors = data['errors'];

      return RegisterErrorDetailsServiceModel(
        succeeded: false,
        emailErrors: errors['Email']?.cast<String>(),
        passwordErrors: errors['Password']?.cast<String>(),
        usernameErrors: errors['Username']?.cast<String>(),
        confirmPasswordErrors: errors['ConfirmPassword']?.cast<String>(),
      );
    } else if (data.containsKey('errors')) {
      // This is the JSON response that you provided
      List<String> usernameErrors = [];
      List<String> emailErrors = [];

      for (var error in data['errors']) {
        if (error['key'] == 'DuplicateUserName') {
          usernameErrors.add(error['value']);
        } else if (error['key'] == 'DuplicateEmail') {
          emailErrors.add(error['value']);
        }
      }

      return RegisterErrorDetailsServiceModel(
        succeeded: false,
        usernameErrors: usernameErrors,
        emailErrors: emailErrors,
      );
    }

    return RegisterErrorDetailsServiceModel();
  }
}
