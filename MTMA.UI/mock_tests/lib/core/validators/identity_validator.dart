class IdentityValidator {
  static String? validateUsername(String? value) {
    if (value == null || value.isEmpty) {
      return "Please enter a username";
    }
    if (value.length < 3) {
      return "Username must be at least 3 characters long";
    }
    if (value.length > 20) {
      return "Username must be less than 20 characters long";
    }
    return null;
  }

  static String? validateEmail(String? value) {
    if (value == null || value.isEmpty) {
      return "Please enter an email address";
    }
    if (!value.contains('@')) {
      return "Please enter a valid email address";
    }
    if (value.length > 50) {
      return "Email address must be less than 50 characters long";
    }
    if (value.length < 3) {
      return "Email address must be at least 5 characters long";
    }
    return null;
  }

  static String? validatePassword(String? value) {
    if (value == null || value.isEmpty) {
      return "Please enter a password";
    }
    if (value.length < 6) {
      return "Password must be at least 6 characters long";
    }
    if (value.length > 32) {
      return "Password must be less than 32 characters long";
    }
    if (value.contains(' ')) {
      return "Password cannot contain spaces";
    }
    if (!value.contains(RegExp(r'\d'))) {
      return "Password must contain at least one number";
    }
    if (!value.contains(RegExp(r'[A-Z]'))) {
      return "Password must contain at least one uppercase letter";
    }
    if (!value.contains(RegExp(r'[a-z]'))) {
      return "Password must contain at least one lowercase letter";
    }
    return null;
  }

  static String? validateConfirmPassword(String? value, {String? password}) {
    if (value == null || value.isEmpty) {
      return "Please confirm your password";
    }
    if (value != password) {
      return "Passwords do not match";
    }
    return null;
  }
}
