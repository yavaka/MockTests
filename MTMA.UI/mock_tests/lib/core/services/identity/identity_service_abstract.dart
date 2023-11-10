import 'package:mock_tests/core/service_models/identity/login_response_service_model.dart';
import 'package:mock_tests/core/service_models/identity/login_service_model.dart';
import 'package:mock_tests/core/service_models/identity/register_error_details_service_model.dart';
import 'package:mock_tests/core/service_models/identity/register_user_service_model.dart';

abstract class IdentityServiceAbstract {
  Future<RegisterErrorDetailsServiceModel> register(RegisterServiceModel serviceModel);
  Future<LoginResponseServiceModel> login(LoginServiceModel serviceModel);
}
