#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Resources
{
    using Extensions.Core.Resources;

    /// <summary>
    /// 身份错误描述器资源。
    /// </summary>
    public class IdentityErrorDescriberResource : IResource
    {
        /// <summary>
        /// Optimistic concurrency failure, object has been modified.
        /// </summary>
        public string ConcurrencyFailure { get; set; }

        /// <summary>
        /// An unknown failure has occurred.
        /// </summary>
        public string DefaultError { get; set; }

        /// <summary>
        /// Email '{0}' is already taken.
        /// </summary>
        public string DuplicateEmail { get; set; }

        /// <summary>
        /// Role name '{0}' is already taken.
        /// </summary>
        public string DuplicateRoleName { get; set; }

        /// <summary>
        /// User name '{0}' is already taken.
        /// </summary>
        public string DuplicateUserName { get; set; }

        /// <summary>
        /// Email '{0}' is invalid.
        /// </summary>
        public string InvalidEmail { get; set; }

        /// <summary>
        /// Type {0} must derive from {1}&lt;{2}&gt;.
        /// </summary>
        public string InvalidManagerType { get; set; }

        /// <summary>
        /// The provided PasswordHasherCompatibilityMode is invalid.
        /// </summary>
        public string InvalidPasswordHasherCompatibilityMode { get; set; }

        /// <summary>
        /// The iteration count must be a positive integer.
        /// </summary>
        public string InvalidPasswordHasherIterationCount { get; set; }

        /// <summary>
        /// Role name '{0}' is invalid.
        /// </summary>
        public string InvalidRoleName { get; set; }

        /// <summary>
        /// Invalid token.
        /// </summary>
        public string InvalidToken { get; set; }

        /// <summary>
        /// User name '{0}' is invalid, can only contain letters or digits.
        /// </summary>
        public string InvalidUserName { get; set; }

        /// <summary>
        /// A user with this login already exists.
        /// </summary>
        public string LoginAlreadyAssociated { get; set; }

        /// <summary>
        /// AddIdentity must be called on the service collection.
        /// </summary>
        public string MustCallAddIdentity { get; set; }

        /// <summary>
        /// No IUserTwoFactorTokenProvider&lt;{0}&gt; named '{1}' is registered.
        /// </summary>
        public string NoTokenProvider { get; set; }

        /// <summary>
        /// User security stamp cannot be null.
        /// </summary>
        public string NullSecurityStamp { get; set; }

        /// <summary>
        /// Incorrect password.
        /// </summary>
        public string PasswordMismatch { get; set; }

        /// <summary>
        /// Passwords must have at least one digit ('0'-'9').
        /// </summary>
        public string PasswordRequiresDigit { get; set; }

        /// <summary>
        /// Passwords must have at least one lowercase ('a'-'z').
        /// </summary>
        public string PasswordRequiresLower { get; set; }

        /// <summary>
        /// Passwords must have at least one non alphanumeric character.
        /// </summary>
        public string PasswordRequiresNonAlphanumeric { get; set; }

        /// <summary>
        /// Passwords must have at least one uppercase ('A'-'Z').
        /// </summary>
        public string PasswordRequiresUpper { get; set; }

        /// <summary>
        /// Passwords must be at least {0} characters.
        /// </summary>
        public string PasswordTooShort { get; set; }

        /// <summary>
        /// Role {0} does not exist.
        /// </summary>
        public string RoleNotFound { get; set; }

        /// <summary>
        /// Store does not implement IQueryableRoleStore&lt;TRole&gt;.
        /// </summary>
        public string StoreNotIQueryableRoleStore { get; set; }

        /// <summary>
        /// Store does not implement IQueryableUserStore&lt;TUser&gt;.
        /// </summary>
        public string StoreNotIQueryableUserStore { get; set; }

        /// <summary>
        /// Store does not implement IRoleClaimStore&lt;TRole&gt;.
        /// </summary>
        public string StoreNotIRoleClaimStore { get; set; }

        /// <summary>
        /// Store does not implement IUserAuthenticationTokenStore&lt;User&gt;.
        /// </summary>
        public string StoreNotIUserAuthenticationTokenStore { get; set; }

        /// <summary>
        /// Store does not implement IUserClaimStore&lt;TUser&gt;.
        /// </summary>
        public string StoreNotIUserClaimStore { get; set; }

        /// <summary>
        /// Store does not implement IUserConfirmationStore&lt;TUser&gt;.
        /// </summary>
        public string StoreNotIUserConfirmationStore { get; set; }

        /// <summary>
        /// Store does not implement IUserEmailStore&lt;TUser&gt;.
        /// </summary>
        public string StoreNotIUserEmailStore { get; set; }

        /// <summary>
        /// Store does not implement IUserLockoutStore&lt;TUser&gt;.
        /// </summary>
        public string StoreNotIUserLockoutStore { get; set; }

        /// <summary>
        /// Store does not implement IUserLoginStore&lt;TUser&gt;.
        /// </summary>
        public string StoreNotIUserLoginStore { get; set; }

        /// <summary>
        /// Store does not implement IUserPasswordStore&lt;TUser&gt;.
        /// </summary>
        public string StoreNotIUserPasswordStore { get; set; }

        /// <summary>
        /// Store does not implement IUserPhoneNumberStore&lt;TUser&gt;.
        /// </summary>
        public string StoreNotIUserPhoneNumberStore { get; set; }

        /// <summary>
        /// Store does not implement IUserRoleStore&lt;TUser&gt;.
        /// </summary>
        public string StoreNotIUserRoleStore { get; set; }

        /// <summary>
        /// Store does not implement IUserSecurityStampStore&lt;TUser&gt;.
        /// </summary>
        public string StoreNotIUserSecurityStampStore { get; set; }

        /// <summary>
        /// Store does not implement IUserAuthenticatorKeyStore&lt;User&gt;.
        /// </summary>
        public string StoreNotIUserAuthenticatorKeyStore { get; set; }

        /// <summary>
        /// Store does not implement IUserTwoFactorStore&lt;TUser&gt;.
        /// </summary>
        public string StoreNotIUserTwoFactorStore { get; set; }

        /// <summary>
        /// Recovery code redemption failed.
        /// </summary>
        public string RecoveryCodeRedemptionFailed { get; set; }

        /// <summary>
        /// User already has a password set.
        /// </summary>
        public string UserAlreadyHasPassword { get; set; }

        /// <summary>
        /// User already in role '{0}'.
        /// </summary>
        public string UserAlreadyInRole { get; set; }

        /// <summary>
        /// User is locked out.
        /// </summary>
        public string UserLockedOut { get; set; }

        /// <summary>
        /// Lockout is not enabled for this user.
        /// </summary>
        public string UserLockoutNotEnabled { get; set; }

        /// <summary>
        /// User {0} does not exist.
        /// </summary>
        public string UserNameNotFound { get; set; }

        /// <summary>
        /// User is not in role '{0}'.
        /// </summary>
        public string UserNotInRole { get; set; }

        /// <summary>
        /// Store does not implement IUserTwoFactorRecoveryCodeStore&lt;User&gt;.
        /// </summary>
        public string StoreNotIUserTwoFactorRecoveryCodeStore { get; set; }

        /// <summary>
        /// Passwords must use at least {0} different characters.
        /// </summary>
        public string PasswordRequiresUniqueChars { get; set; }

        /// <summary>
        /// No RoleType was specified, try AddRoles&lt;TRole&gt;().
        /// </summary>
        public string NoRoleType { get; set; }

        /// <summary>
        /// Store does not implement IProtectedUserStore&lt;TUser&gt; which is required when ProtectPersonalData = true.
        /// </summary>
        public string StoreNotIProtectedUserStore { get; set; }

        /// <summary>
        /// No IPersonalDataProtector service was registered, this is required when ProtectPersonalData = true.
        /// </summary>
        public string NoPersonalDataProtector { get; set; }

        /// <summary>
        /// No IPersonalDataProtector service was registered, this is required when ProtectPersonalData = true.
        /// </summary>
        public string FormatNoPersonalDataProtector { get; set; }
    }
}
