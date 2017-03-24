VAR found_book = false
VAR john_is_an_idiot = false
VAR getting_out_of_town = false
VAR day1_Complete = false



===Prisoner===
=Pop_Up
Sheriff!
->DONE

=Conversation
Come on, Sheriff. You know you don’ got nuthin’ on me. Might as well let me go now, yeah? Either way, I’m gonna be a free man soon.”
*[You're gonna stay here for a long time]
    “You keep thinking that, Bob. I’ve got enough on you for that mine deal that will keep you in here for a long time to come. You’re lucky you’re not being hanged for your involvement.”
-"Bah! Yer jist bluffin’....”
*["I don’t bluff, Bob."]
    "Unlike you."
    ->DONE

===Pemberton
“Sheriff Rawley?”
    *[Yes?Can I Help you]
    “I’m hoping you can, Sheriff. There’s been a bit of a...problem going down in Crowstone, and I need your help in handling it.”
        **[“What kind of problem?”]
            “I’m afraid I’m not at liberty to elaborate too much on the subject of my assignment. It’s a matter of national security, you understand. What I can say is that I’m in search of a wanted fugitive, and my agency believes he’s holed up here. If it gets out that I’m in town, my suspect may catch wind of it and run. A man in your position can surely sympathize.”  ->continue
        **(agent) [“I’m sorry...who are you?”]
        “I’m an undercover agent with the Pemberton International Detective Agency. Certain folk in your town are starting to talk a little too much about me since I arrived, and I could really use your help in keeping them quiet about my presence.”
            ***{agent} [“Hold up. You’re an agent? What could you possibly be investigating in our town?”]
                “I’m afraid I’m not at liberty to elaborate too much on the subject of my assignment. It’s a matter of national security, you understand. What I can say is that I’m in search of a wanted fugitive, and my agency believes he’s holed up here. If it gets out that I’m in town, my suspect may catch wind of it and run. A man in your position can surely sympathize.”
    - -> continue
= continue
    * [What's in it for me?]
        Beyond having the satisfaction of knowing you aided your government? Well, you'll be compensated, of course. Money speaks to you, I assume.
        ** [What do you need me to do?]
            A man by the name of John "The Gambler" Jones, has recently become aware of my presence in town. I need you to guarantee his cooperation before my cover is compromised.
            *** [How can I do that?]
                However you can. If you think you're skilled enough, you can try to persuade him. If that doesn't work, try investigating him.
                **** [Do you have any investigation tips?]
                        Try talking to your townsfolk to find out more about him, or you can look around for evidence you can use against him. Do whatever you can to keep him quiet.
                **** [I'm on it]
                    ->DONE
            *** [I'm on it]
                    ->DONE
->DONE
//*[“What’s in it for me?”]
===Book===
    ~found_book = true 
->DONE

===Gambler===
Sheriff! What do you want?
    + [I heard you’ve been asking a lot of questions about the new man in town.] The one with the red hat. I need you to stop.
            That’s none of your business.
            **{john_is_an_idiot} [I think he’s a threat to you, John.]
                What do you mean?
                    ->threat
            **{found_book} [I think it is my business.]
                    (Show  ledger)
                    Is that my ledger?! Where did you get that?!
                    ***[That doesn't matter. If you want it, you need to  leave town for a little while.]
                            Fine, I'll go! Just give it back to me!
                            ~ getting_out_of_town = true
                            ->DONE
                    ***[Where you left it. But you can have it back.]
                            How can I repay you, Sheriff?
                            ****[Stop asking questions about the new guy in town.]
                                    You have yourself a deal, Sheriff.
                                    ~ getting_out_of_town = true
                                    ->DONE
            ++ [I guess you're right.]
                ->DONE
    *[What have you been up to?]
        Why do you care?
         **The man you've been following is a danger to you!
            What?!
             ->threat
             
= threat
*[I overheard him earlier today saying that he’s looking for you.] Something about you owing him money? I think he’s fixing to kill you. I’d feel a lot better if you left town for a while.
                            Thanks, Sheriff! I’d best be getting out of here! 
                                  ~ getting_out_of_town = true 
                                  ->DONE
===Gambler2===
Sheriff! What do you want?
*{found_book} found your book
    ->book_talk
*I heared you've been asking questions
    What about it? 
    **Nothing
        ->DONE
*{john_is_an_idiot} Aliens!#Aliens
    ~ getting_out_of_town = true
    ->DONE


=book_talk
My book! Give it to me!
* Sure #gave_book_wilingly
    Thank you Sheriff!
    ->DONE
* Not so fast 
    What do you want?
    **For you to get out of town! #used_threat
        I always disliked you Sheriff 
        ~ getting_out_of_town = true
        ->DONE
    **A hug #used_love
    `   there you go Sheriff
    - ->DONE
    

===Pemberton_Return===
Well, Sheriff? Were you able to silence your local gambler?
    *[Yes, John won't be a problem now.]
- I knew I could count on you, Sheriff.
    *[Is there anything else I can do to help?]
-Maybe tomorrow. I'll have to see if I can find any other people for you to investigate.
    *Right. I'll talk to you tomorrow, then. 
        ~ day1_Complete = true
        -> DONE   
===Bartender===
Hey, Sheriff. What can I do for you?”
->question 
=question 
+ "Can I get a drink?"
    I don’t think you can afford one right now,
*“What do you know about John?”
    ->ask_about_john 
+Nothing, thanks.
    ->DONE
- anything else I can do for you Sheriff?
->question
=ask_about_john
Depends, What do you want to know about him?”
-(ask_more)
*“What do you know about him?” 
    ~ john_is_an_idiot = true
    “He’s a nice enough fella, but he ain’t too bright. 
*Have you ever noticed anything odd about him?
    “Well, I dunno if it’s odd, but I know he usually carries a book around with him. I guess it’s important to him."
+Nothing, I'm good
    ->DONE
-Is there anything else you need to know about him?"
->ask_more


