import 'dart:convert';

import 'package:http/http.dart';
import 'package:mock_tests/app_settings.dart';
import 'package:mock_tests/core/service_models/identity/login_response_service_model.dart';
import 'package:mock_tests/core/service_models/identity/login_service_model.dart';
import 'package:mock_tests/core/service_models/identity/register_error_details_service_model.dart';
import 'package:mock_tests/core/service_models/identity/register_user_service_model.dart';
import 'package:mock_tests/core/services/identity/identity_service_abstract.dart';

class IdentityService extends IdentityServiceAbstract {
  @override
  Future<RegisterErrorDetailsServiceModel> register(RegisterServiceModel serviceModel) async {
    var result = await post(
      Uri.parse('$apiBaseUrl/identity/register'),
      headers: <String, String>{
        'Content-Type': 'application/json',
      },
      body: jsonEncode(serviceModel.toJson),
    );

    if (result.statusCode == 200) {
      return RegisterErrorDetailsServiceModel(succeeded: true);
    }

    var errors = RegisterErrorDetailsServiceModel.fromJson(result.body);
    return errors;
  }

  @override
  Future<LoginResponseServiceModel> login(LoginServiceModel serviceModel) async {
    var response = await post(
      Uri.parse('$apiBaseUrl/identity/login'),
      headers: <String, String>{
        'Content-Type': 'application/json',
      },
      body: jsonEncode(serviceModel.toJson),
    );

    return LoginResponseServiceModel.fromJson(response.body);
  }
}
