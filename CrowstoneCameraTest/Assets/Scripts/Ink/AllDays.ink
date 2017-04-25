VAR found_book = false
VAR day = 1
VAR john_is_an_idiot = false
VAR gambler_ran = false
VAR day_Complete1 = false
VAR met_pemberton_day1 = false


VAR day_Complete2 = false
VAR wanted_Poster = false
VAR showed_Poster_Kathrine = false
VAR met_pemberton_day2 = false
VAR met_Morgen = false
VAR morgan_Ran = false

VAR day_Complete3 = false
VAR met_pemberton_day3 = false
VAR adelaida_ran = false
VAR Currency = 0
VAR heared_rumor =false
VAR san_fran = false
VAR currTask = ""

===CheckTask===
{
    -day ==1 :
        { met_pemberton_day1: 
                    {gambler_ran:
                        {day_Complete1:
                            ~currTask = "Leave office"
                        -else:    
                        ~currTask = "Report back to Pemberton"
                        }
                    -else:
                        ~currTask = "Take care of John"
                    }
            -else:
                ~currTask = "Go to the Sheriff's office"
        }
    -day == 2:
        {met_pemberton_day2:
            {morgan_Ran:
                        {day_Complete2:
                            ~currTask = "Leave office"
                        -else:    
                        ~currTask = "Report back to Pemberton"
                        }
                    -else:
                ~currTask = "Take care of Morgen"
            }
          -else: 
            ~currTask = "Go to the Sheriff's office"
        }
    -day == 3:
        {met_pemberton_day3:
            {adelaida_ran:
                        {day_Complete3:
                            ~currTask = "Leave office"
                        -else:    
                        ~currTask = "Report back to Pemberton"
                        }
                    -else:
                ~currTask = "Take care of Adelaida"
            }
          -else: 
            ~currTask = "Go to the Sheriff's office"
        }
}
->DONE

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

===Pemberton===

{
    -day == 1: ->day1
    -day == 2: ->day2
    -day == 3: -> day3
}

=day1
{met_pemberton_day1: ->second_meeting|->first_meeting}
=first_meeting
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
                        *****[I'm on it]
                             ~ met_pemberton_day1 = true
                             ->DONE
                            
                **** [I'm on it]
                    ~ met_pemberton_day1 = true
                    ->DONE
            *** [I'm on it]
                    ~met_pemberton_day1 = true
                    ->DONE
                    
= second_meeting
{day_Complete1: ->end_day1}
Well, Sheriff? Were you able to silence your local gambler?
    *{gambler_ran}[Yes, John won't be a problem now.]
        I knew I could count on you, Sheriff.
            **[Is there anything else I can do to help?]
                Maybe tomorrow. I'll have to see if I can find any other people for you to investigate.
                    ***[Right. I'll talk to you tomorrow, then.] 
                        ~ day_Complete1 = true
                        Head home and get some rest Sheriff. -> DONE  
    +[I'm Handling it]
        ->DONE
= end_day1
Head home and get some rest Sheriff. -> DONE
        
        
=day2
{met_pemberton_day2:->second_meeting2 |->first_meeting2}

=first_meeting2
Good morning, Sheriff. I'm glad to see you're an early riser. I have a new lead for you to follow. There's a man in town, Jacob Morgan. I believe he's caught wind of my presence. ->converstation_options
=converstation_options
*[Have any tips on how I should handle him?]
    He seems a hard man, so I would suggest that in addition to trying to persuade him, you could try the route of blackmail. I'll bet he has some dirt on him. He seems the type. Of course, you're always welcome to try the other options we discussed yesterday. -> converstation_options
+[I'd best get to work, then.]
    ~met_pemberton_day2 = true
    ->DONE
    
=second_meeting2
{day_Complete2: ->end_day2}
I trust you were successful again, Sheriff?
+[Not yet]
    ->DONE
*{morgan_Ran}[Yes, Jacob won't be a problem anymore.]
    Another job well done. You're proving to be quite the ally, Sheriff. I'm going to do a little more detective work on another of your citizens. Come back tomorrow and I should have more information for you.
    **[Right. Until then.]
        ~day_Complete2 = true
        ->DONE
=end_day2
See you tomorrow Sheriff
->DONE

=day3

{met_pemberton_day3: ->second_meeting3|->first_meeting3}

=first_meeting3
Sheriff, good morning! I'm glad you're here. My suspicions were correct. There's a woman in town, Adelaida Santorez. She knows about me. I need you to guarantee her silence.
    *[Do you have any investigation tips for her?]
        She seems the type that might be bribed. You can try that in addition to your other skills. I trust you can help me with her, Sheriff. You don't seem to be the rich type so let me give you a small loan *recive $500*
            ~ Currency = 500 
                **[I'm on it]
                    ~ met_pemberton_day3 = true
                    ->DONE    
    *[I'm on it]
        ~ met_pemberton_day3 = true
        ->DONE
=second_meeting3
{day_Complete3: ->end_day3}
Sheriff, you were successful in silencing Adelaida?
+[Still working on it]
    ->DONE
*{adelaida_ran}[Yes, she won't be asking about you anymore.]
                I knew I could count on you. Thank you for all your help.
                **[It's no problem.]
                    ~day_Complete3 = true
                    ->DONE
=end_day3
Head home and get spme rest Sheriff
->DONE

===Book===
    ~found_book = true 
->DONE

===Gambler===
{met_pemberton_day1: ->coneversation | ->busy}
=busy
Sorry sherrif but I'm a little preoccupied at the moment.
    +[Stay out of trouble]
        ->DONE
=coneversation
{gambler_ran: ->gambler_silenced}
Sheriff! What do you want?
    +[I heard you’ve been asking a lot of questions about the new man in town. The one with the red hat. I need you to stop.]
            That’s none of your business.
                **{john_is_an_idiot} [I think he’s a threat to you, John.]
                What do you mean?
                    ->threat
                **{found_book}[I think it is my business.]
                    (Show  ledger) Is that my ledger?! Where did you get that?!
                    ***[That doesn't matter. If you want it, you need to  leave town for a little while.]
                            Fine, I'll go! Just give it back to me!
                            ~ gambler_ran = true
                            ->DONE
                    ***[Where you left it. But you can have it back.]
                            How can I repay you, Sheriff?
                            ****[Stop asking questions about the new guy in town.]
                                    You have yourself a deal, Sheriff.
                                    ~ gambler_ran = true
                                    ->DONE
            ++ [I guess you're right.]
                ->DONE
    *[What have you been up to?]
        Why do you care?
         **The man you've been following is a danger to you!
            What?!
             ->threat
             
= threat
*[I overheard him earlier today saying that he’s looking for you. Something about you owing him money? I think he’s fixing to kill you. I’d feel a lot better if you left town for a while.]
                            Thanks, Sheriff! I’d best be getting out of here! 
                                  ~ gambler_ran = true 
                                  ->DONE

=gambler_silenced
I'm going to avoid that man like the plague!
->DONE
===Bartender===
{
    -day == 1: ->day1
    -day == 2: ->day2
    -day == 3: -> day3
}
=day1
Hey, Sheriff. What can I do for you?”
->question 
=question 
+ ["Can I get a drink?"]
    I don’t think you can afford one right now,
*{met_pemberton_day1}[“What do you know about John?”]
    ->ask_about_john 
+[Nothing, thanks.]
    ->DONE
- anything else I can do for you Sheriff?
->question
=ask_about_john
Depends, What do you want to know about him?”
-(ask_more)
*[“What do you know about him?”] 
    ~ john_is_an_idiot = true
    “He’s a nice enough fella, but he ain’t too bright. 
*[Have you ever noticed anything odd about him?]
    “Well, I dunno if it’s odd, but I know he usually carries a book around with him. I guess it’s important to him."
+[Nothing, I'm good]
    ->DONE
-Is there anything else you need to know about him?"
->ask_more

=day2
Hey, Sheriff. What can I do for you?”
->question2 
=question2 
+ ["Can I get a drink?"]
    I don’t think you can afford one right now
    ->question2
+{met_pemberton_day2}[What can you tell me about Jacob Morgan?]
    Jacob Morgan? Oh, yes! He's the new man in town, right? The rugged fellow. I know a little bit, Sheriff. What would you like to know about him? I'll see if I can help. ->investigate2
+[Nothing, thanks.]
    ->DONE
=investigate2
    
*[What is he like?]
        Hard to tell, really. He's a firm man, and he rarely smiles. I've noticed he looks an awful lot like one of the wanted posters you have hanging up on your wall. 'Black Jack?' But surely that can't be him.... Is there anything else you need to know?->investigate2
*[Do you know who he associates with in town?]
        Between you, me, and the tree, Sheriff, I've seen him over at Katherine Blakley's school. I think he fancies her. Isn't young love sweet? What else can I do you for, Sheriff?->investigate2
+[Nothing for now. have a good day Henry]
    ->DONE
    
=day3

->intial

=intial
Sheriff, I trust you're not here to arrest anyone. What can I do for you?->talk
=talk
+ [Can I get a drink?]
    {Currency >= 50: ->get_drink|I don’t think you can afford one right now. ->talk}
+{met_pemberton_day3}[What can you tell me about Adelaida?]
    You're nosy, aren't you? I guess I can help you out. What do you need to know?
    ->investigate
+[I'm good for now]
    ->DONE
=investigate
*[What is she like?]
     Are you serious? Haven't you seen her flirting around with every man in town? Ever since her husband left her for that other woman, she's become a jealous vixen. -> investigate
*[Have you heard any rumors about her?]
    ~heared_rumor=true
    Only that she's desperate to get out of town. Something about moving to San Francisco and marrying a rich man. Don't know how she's going to do it if she keeps bouncing around the men here. None of them are taking her there, that's for sure. You know, I think I saw her giving something to Albert Moorsby in the mercantile store. In fact, she's been giving a lot of things to a lot of the men. Might try talking to Albert. I'll bet it's a love token or something. If that's true, she's cheating on all her suitors, the harlot! ->investigate
+[I think that's enough for now]
    ->DONE
    
=get_drink 
 ~Currency = Currency - 50
 Don't over due it Sheriff.
 Anything else I can do for you?
 ->talk
 
 ===Jacob_Morgen===
 {met_pemberton_day2==false:->busy}
 
{met_Morgen: ->second_meeting|->first_meeting}
=busy
Can't you see I'm busy here Rawley
 +Stay out of trouble Morgen...
    ->DONE
 
=first_meeting
What the hell do you want, Sheriff? Can't a man walk the streets without a lawman riding his coat tails.
* [I hear you've been poking your nose into business that isn't yours, Jacob.] The man in the red hat...I need you to stop talking about him.
        And why should I listen to you? Who cares if it's my business or not? It's a free country, last I checked. I can talk about anybody I want to.
        **[I'll be back to continue this conversation later.]
            ~met_Morgen =true
            ->DONE
=second_meeting
{morgan_Ran: ->silenced_morgen}
Back again, Sheriff? I thought I made it clear that I didn't want to talk to you.
*{wanted_Poster} [Jacob, I found this. Care to explain how your face got on this wanted poster?]
    What?! Now, Sheriff...let's not be hasty here. I'm not lookin' to hang today....
    **[Then you'd better leave town, Jacob. I don't want to see you around here for a while.]
        ~morgan_Ran =true
        Right, Sheriff. I got some loose ends to tie up today, then I'll get outta your hair. I swear.
        ->DONE
+[I'll be back Morgen]
    ->DONE
    
=silenced_morgen
You'll never see me again Sheriff, I swear!
->DONE

===wanted_Poster===
    ~wanted_Poster = true
    ->DONE
            
===Harriet===
Well, good day to you, Sheriff. What can I do you for?
+[Nothing for now. have a good day Miss.]
    ->DONE
+[What can you tell me about Jacob Morgan?]
    Jacob Morgan? Oh, yes! He's the new man in town, right? The rugged fellow. I know a little bit, Sheriff. What would you like to know about him? I'll see if I can help. ->investigate
=investigate
    
*[What is he like?]
        Hard to tell, really. He's a firm man, and he rarely smiles. I've noticed he looks an awful lot like one of the wanted posters you have hanging up on your wall. 'Black Jack?' But surely that can't be him.... Is there anything else you need to know?->investigate
*[Do you know who he associates with in town?]
        Between you, me, and the tree, Sheriff, I've seen him over at Katherine Blakley's school. I think he fancies her. Isn't young love sweet? What else can I do you for, Sheriff?->investigate
+[Nothing for now. have a good day Henry]
    ->DONE
    
    
===Katherine_Blakely===
Good day, Sheriff! You don't make your way out this way too often. What can I do for you?

*{wanted_Poster}{met_pemberton_day2}[I understand you've been seeing Jacob Morgan]
    Wh-what is this...? A wanted poster? With Jacob on it? This can't be...I thought I knew him. I need to speak to him right now. Thank you, Sheriff. ->DONE
    
+[Nothing for now Ms. Blakley]
    ->DONE
    

===Adelaida===
{adelaida_ran: ->busy|->intialize}
=intialize
Well, hello there, Sheriff. What's a fine man like yourself doing out here in this heat?->talk
=talk
*{heared_rumor}[I hear you're looking to leave town. Why's that?]
    ~san_fran = true
    Isn't it obvious? There's nothing here for me in this pathetic town. There's no love, no money. A woman has needs, you know? Ever since my husband left me, I have nothing! There's only one place in this world worthy of a woman like me: San Francisco!->talk
*{san_fran} [About San-Francisco...]
            ->san_fran_talk
*{heared_rumor}[I hear you've been going around town and seducing all the men]
                That's private! Where di you hear that?!
                **[If you want me to keep quiet about this, I need you to do something for me. I know you've been snooping for information on the man in the red hat. I need you to keep to your own business.]
                        ~adelaida_ran = true
                        If you do, I won't ask anymore questions about that man. I swear! ->DONE
                        
+[I have to go]
    ->DONE
=san_fran_talk
Yes?
*{Currency >= 400} [I'd hate to see a woman like you be forced to stay in a town that doesn't deserve you. How about I spot you some money for a coach ride to San Francisco?]
        ~Currency = Currency - 400
        ~adelaida_ran = true
        You'd do that for me, Sheriff?! Aiye, you're a saint! San Francisco, get ready for me!->DONE
+[Never mind]
    ->talk
=busy
Sorry Sheriff I'm to busy planning on getting away from here as far as possible
->DONE



