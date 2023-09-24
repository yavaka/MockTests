import 'package:mock_tests/core/service_models/identity/register_error_details.dart';
import 'package:mock_tests/core/service_models/identity/register_user_service_model.dart';

abstract class IdentityServiceAbstract {
  Future<RegisterErrorDetailsServiceModel> register(RegisterUserServiceModel serviceModel);
}
