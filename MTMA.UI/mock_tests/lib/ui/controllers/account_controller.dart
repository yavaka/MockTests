import 'package:mock_tests/core/service_models/account/dashboard_service_model.dart';
import 'package:mock_tests/core/services/account/account_service_abstract.dart';

class AccountController {
  static final AccountController _instance = AccountController._();

  static late final AccountServiceAbstract _accountService;

  AccountController._();
  factory AccountController(AccountServiceAbstract accountService) {
    _accountService = accountService;
    return _instance;
  }

  DashboardServiceModel? getDashboard() {
    return _accountService.getDashboard();
  }
}
