import 'dart:convert';

import 'package:http/http.dart';
import 'package:mock_tests/app_settings.dart';
import 'package:mock_tests/core/service_models/identity/register_error_details.dart';
import 'package:mock_tests/core/service_models/identity/register_user_service_model.dart';
import 'package:mock_tests/core/services/identity/identity_service_abstract.dart';

class IdentityService extends IdentityServiceAbstract {
  @override
  Future<RegisterErrorDetailsServiceModel> register(RegisterUserServiceModel serviceModel) async {
    var data = jsonEncode(serviceModel.toJson);

    var result = await post(
      Uri.parse('$apiBaseUrl/identity/register'),
      headers: <String, String>{
        'Content-Type': 'application/json',
      },
      body: data,
    );

    if (result.statusCode != 200) {
      var errors = RegisterErrorDetailsServiceModel.fromJson(result.body);
      return errors;
    } else {
      return RegisterErrorDetailsServiceModel(succeeded: true);
    }
  }
}
