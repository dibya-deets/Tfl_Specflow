Feature: Plan your journey in TFL
    Description: The purpose of the feature is to test the plany your journey widget of tfl website
    Background: User opens browser and navigate to tfl site
        Given user is on tfl site
        When user selects plan a journey option
        Then he should lands on plan a journey widget

#Test case1
    Scenario Outline: Verify if user plans a valid journey
        When he enters from_address as <from_address>
        And he enters to_address as <to_address>
        Then he clicks on plan a journey button
        Then he should be able to verify his journey result
        Examples:
            | from_address | to_address |
            # with one valid address
            | Stratford    | Ilford     |
            # with one invalid address
            | @            | 2          |
 

#Test case 2
    Scenario: Verify that the widget is unable to plan a journey if no locations are entered into the widget.
        When he leaves from_address and to_address blank
        Then he clicks on plan a journey button
        Then user receives an error message


#Test case 3
    Scenario Outline: Verify if user can edit his journey result
        When he enters from_address as <from_address>
        And he enters to_address as <to_address>
        Then he clicks on plan a journey button
        Then he should be able to verify his journey result
        When he clicks on edit journey option
        Then he navigates to Edit journey page and enterd new from_address
        And he should see the updated journey result

        Examples:
            | from_address | to_address |
            | Seven Kings  | Ilford     |



#Test case 4
    Scenario: Verify whether user can view his recent planned journey
       When he enters from_address as <from_address>
        And he enters to_address as <to_address>
        Then he clicks on plan a journey button
        And navigate to plan a journey page
        When user selects plan a journey option
        When user selects recent option
        Then he should see list of recent searched journey

             Examples:
            | from_address | to_address |
            | Stratford    | Ilford     |
     



