import 'package:mock_tests/core/service_models/identity/register_error_details.dart';
import 'package:mock_tests/core/service_models/identity/register_user_service_model.dart';
import 'package:mock_tests/core/services/identity/identity_service_abstract.dart';

class IdentityController {
  static final IdentityController _instance = IdentityController._();

  static late final IdentityServiceAbstract _identityService;

  IdentityController._();
  factory IdentityController(IdentityServiceAbstract identityService) {
    _identityService = identityService;
    return _instance;
  }

  Future<RegisterErrorDetailsServiceModel> register(RegisterUserServiceModel serviceModel) async {
    return await _identityService.register(serviceModel);
  }
}
