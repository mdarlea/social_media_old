@ignore
Feature: Call Member And Say Verification Code
	In order to communicate a verification code to a member
	As a Twilio admin
	I want to successfully call the member's phone number


Scenario: LOCAL: Call the member to communicate the verfication code
	Given the following information:
		| Field                  | Value                                                          |
		| TwilioUrl              | http://localhost:31444/api/OutboundCall/VerificationCode.json/ |
		| VerificationCodeLength | 7                                                              |
	When I call the member at '813-340-6560'
	Then A new verification code with a length equal to 7 should be generated
	And the newly created verification code should be communicated to the applicant
		
Scenario: Call the member to communicate the verfication code
	Given the following information:
		| Field                  | Value                                                                          |
		| TwilioUrl              | http://gte-messaging.azurewebsites.net/api/OutboundCall/VerificationCode.json/ |	
		| VerificationCodeLength | 7                                                                              |	
	When I call the member at '813-340-6560'
	Then A new verification code with a length equal to 7 should be generated
	And the newly created verification code should be communicated to the applicant
