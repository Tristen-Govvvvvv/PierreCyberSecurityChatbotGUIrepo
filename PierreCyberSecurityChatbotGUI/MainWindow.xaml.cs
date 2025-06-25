using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using PierreCyberSecurityBotPROG;

namespace PierreCyberSecurityChatbotGUI
{
    public partial class MainWindow : Window
    {
        private readonly KeywordResponseManager responseManager;
        private readonly RandomResponseManager randomResponseManager;
        private readonly UserMemory userMemory = new UserMemory();
        private readonly ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
        private readonly List<QuizQuestion> quizQuestions = new List<QuizQuestion>();
        private int currentQuizQuestion = -1;
        private int quizScore = 0;
        private readonly Random random = new Random();
        private readonly List<ActivityLogEntry> activityLog = new List<ActivityLogEntry>();
        private const int MaxLogEntries = 10;

        private readonly List<string> defaultResponses = new List<string>
        {
            "Ummm...could you run that by me one more time?",
            "I didn't catch that could you try asking again?",
            "Either you just typed some utter nonsense or my developer hasn't added this to the dictionary...it's probably the second one",
            "I wasn't programmed to understand typos...take your time and make sure the spelling is correct."
        };

        private readonly Dictionary<string, string> toneResponses = new Dictionary<string, string>
        {
            ["worried"] = "okay okay lets take deep breath in and and then out...saftey is a massive concern for everyone so i completely get that you are strssing about this. Let break it down for you. ",
            ["frustrated"] = "Yeah I get it..Tech can be annoying asl sometimes..but spazzing out at me helps nobody. That being said the Bible teaches forgiveness so lets put it past us and let me simplify this for you. ",
            ["curious"] = "You remind me of a monkey named George; he was really curious too...anyways lets talk about this ",
            ["happy"] = "YESSIRR! Thats what im here for...lets confirm it one more time for good measure",
            ["depressed"] = "I can see this is really messing with you...okay im here and while i cant promise I'll always fix your problems imma always be here. Lets look it this topic again."
        };

        public MainWindow()
        {
            var keywordResponses = new List<KeywordResponse<string>>
            {
                   new KeywordResponse<string> { Keyword = "how are you", Response = "I mean I could have been coded to help solve world hunger but then again...I could have been someones AI girlfriend..so I'd say im pretty good." },
    new KeywordResponse<string> { Keyword = "what's your purpose", Response = "Ahhh...the meaning of life... I could get really philosophical on you but basically im here to educate you on how to stay safe when online." },
    new KeywordResponse<string> { Keyword = "what can i ask you about", Response = "I can answer and tell you about different topics about cyber security (keep in mind im still in development so some of your questions may not have answers yet)" },
    new KeywordResponse<string> { Keyword = "remember me when the AI take over", Response = "say less." },
    new KeywordResponse<string> { Keyword = "who made you", Response = "Not too sure he went to the app store and never came back" },
    new KeywordResponse<string> { Keyword = "how good is your memory", Response = "Well I'm not one to brag but I'd actually remember to buy you flowers every now and again unlike your mans" },
    new KeywordResponse<string> { Keyword = "add task", Response = "best of luck with your task!" }
            };

            responseManager = new KeywordResponseManager(keywordResponses);
            randomResponseManager = new RandomResponseManager();

            InitializeComponent();
            InitializeGuiFeatures();
            ShowWelcomeSequence();
        }

        private void InitializeGuiFeatures()
        {
            taskList.ItemsSource = tasks;
            InitializeQuizQuestions();
            chatDisplay.Document.Blocks.Clear();
            
        }

        private void InitializeQuizQuestions()
        {
            quizQuestions.Clear(); 

            quizQuestions.AddRange(new[]
            {
        new QuizQuestion
        {
            Question = "1. What's the FIRST thing you should do with a suspicious email?",
            Options = new List<string>
            {
                "Click links to verify legitimacy",
                "Report to IT/security team",
                "Reply asking for verification",
                "Forward to coworkers to warn them"
            },
            CorrectAnswer = 1,
            Explanation = "🚨 Always report phishing attempts - security teams need to track them!"
        },
        new QuizQuestion
        {
            Question = "2. Which password would take longest to crack?",
            Options = new List<string>
            {
                "Password123!",
                "Summer2023",
                "P@$$w0rd",
                "PurpleTurtle$Jumped42!"
            },
            CorrectAnswer = 3,
            Explanation = "💡 Longer passphrases with mixed characters are strongest (12+ characters ideal)"
        },
        new QuizQuestion
        {
            Question = "3. True/False: Opening attachments from strangers is safe if your antivirus is active",
            Options = new List<string> { "True", "False" },
            CorrectAnswer = 1,
            Explanation = "⚠️ Never open unexpected attachments - even antivirus can miss zero-day threats!"
        },
        new QuizQuestion
        {
            Question = "4. Two-factor authentication (2FA) is important because:",
            Options = new List<string>
            {
                "It makes passwords unnecessary",
                "Adds a second verification step",
                "Prevents all hacking attempts",
                "Only needed for banking apps"
            },
            CorrectAnswer = 1,
            Explanation = "🔒 2FA stops 99.9% of automated attacks - enable it everywhere possible!"
        },
        new QuizQuestion
        {
            Question = "5. On public WiFi, you should always:",
            Options = new List<string>
            {
                "Disable firewall for faster speed",
                "Use VPN for sensitive activities",
                "Access work files normally",
                "Share your login credentials"
            },
            CorrectAnswer = 1,
            Explanation = "📶 VPNs encrypt your traffic - essential for airports/cafes/hotels"
        },
        new QuizQuestion
        {
            Question = "6. Encryption protects data by:",
            Options = new List<string>
            {
                "Making files invisible",
                "Scrambling content without a key",
                "Deleting after one view",
                "Slowing down hackers"
            },
            CorrectAnswer = 1,
            Explanation = "🔐 Proper encryption = even stolen data is useless without decryption keys"
        },
        new QuizQuestion
        {
            Question = "7. Backups help against ransomware by:",
            Options = new List<string>
            {
                "Preventing infection",
                "Allowing data restoration",
                "Paying the hackers faster",
                "Making files unreadable"
            },
            CorrectAnswer = 1,
            Explanation = "💾 3-2-1 Backup Rule: 3 copies, 2 media types, 1 offsite copy"
        },
        new QuizQuestion
        {
            Question = "8. Firewalls act like:",
            Options = new List<string>
            {
                "Vaccines preventing disease",
                "Bouncers checking IDs",
                "Speed bumps slowing traffic",
                "Librarians organizing books"
            },
            CorrectAnswer = 1,
            Explanation = "🛡️ Firewalls filter incoming/outgoing traffic based on security rules"
        }
    });
        }

        private void ShowWelcomeSequence()
        {
            
            new Thread(() =>
            {
                try
                {
                    string audioPath = @"C:\Users\lab_services_student\Downloads\PROG mp3 to convert.wav";

                    // Verify file exists before attempting to play
                    if (File.Exists(audioPath))
                    {
                        using (SoundPlayer player = new SoundPlayer(audioPath))
                        {
                            player.Load();  
                            player.PlaySync();  
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Audio file not found at: " + audioPath);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Audio playback error: " + ex.Message);
                    
                }
            }).Start();
           
            string asciiArt = @"
                  .:.               
             .::::.             
..         ..::::::''::         
::::..  .::''''':::    ''.      
':::::::'         '.  ..  '.    
 ::::::'            : '::   :   
  :::::     .        : ':'   :  
  :::::    :::       :.     .'. 
 .::::::    ':'     .' '.:::: : 
 ::::::::.         .    ::::: : 
:::::    '':.... ''      '''' : 
':::: .:'              ...'' :  
 ..::.   '.........:::::'   :   
  '':::.   '::'':'':::'   .'    
        '..  ''.....'  ..'      
           ''........''";

           
            DisplayAsciiArt(asciiArt);

            TypeToChat("AYEEEEE WHATS GOOD HUMAN! I'm Pierre, your favourite Cybersecurity Awareness Bot!", Brushes.Cyan, 40);
            TypeToChat("Stranger danger is the first rule they teach in security so can I get your name?", Brushes.Cyan, 40);
        }

        private void ProcessUserInput()
        {
            string input = userInput.Text.Trim();

            // handles empty input
            if (string.IsNullOrWhiteSpace(input))
            {
                TypeToChat("Come on, work with me here! Give me something to work with!", Brushes.IndianRed, 40);
                return;
            }

            AddUserMessage(input);
            userInput.Clear();

            // handles name input first
            if (string.IsNullOrEmpty(userMemory.Name))
            {
                
                if (input.Length < 2)
                {
                    TypeToChat("You dont really have a choice here tbh. Please enter your REAL name even if its a fake one.", Brushes.IndianRed, 40);
                    return;
                }

                if (input.All(char.IsDigit))
                {
                    TypeToChat("I know I was made really last minute but I was NOT born yesterday", Brushes.IndianRed, 40);
                    return;
                }

                if (input.Equals("Your name", StringComparison.OrdinalIgnoreCase))
                {
                    TypeToChat("you got the whole system laughing...now give me a real name.", Brushes.IndianRed, 40);
                    return;
                }

                userMemory.Name = input;
                TypeToChat($"Alright then {input}! I will show you how to stay safe online...think of me as your Mr. Miyagi \n what would you like to talk about?", Brushes.Cyan, 40);
                LogActivity("User named", $"Set name to: {input}");
                return;
            }

            // activity Log Commands
            if (input.IndexOf("show activity log", StringComparison.OrdinalIgnoreCase) >= 0 ||
                input.IndexOf("what have you done", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                ShowActivityLog();
                return;
            }

            if (input.IndexOf("show full log", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                ShowFullActivityLog();
                return;
            }

            // exit command
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                TypeToChat($"Til we meet again {userMemory.Name}...stay safe and God bless!", Brushes.Cyan, 60);
                Close();
                return;
            }

            
            TryCreateAutoTask(input);

            // detect tone
            string tone = DetectTone(input);
            string tonePrefix = tone != "neutral" ? toneResponses[tone] : "";

            // remembered interests
            var interests = new[] { "privacy", "security", "passwords", "hacking", "malware" };
            foreach (var interest in interests)
            {
                if (input.IndexOf(interest, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!userMemory.Interests.Contains(interest))
                    {
                        userMemory.AddInterest(interest);
                        TypeToChat($"I have really good memory... so I'll remember you're into {interest}.", Brushes.Cyan, 40);
                        LogActivity("Interest added", interest);
                    }
                    else if (randomResponseManager.TryGetRandomResponse(interest, out string personalizedResponse))
                    {
                        TypeToChat($"{tonePrefix}Since you care about {interest}, here's something fresh: {personalizedResponse}", Brushes.Cyan, 40);
                        return;
                    }
                    break;
                }
            }

            bool matched = false;

            // checks random responses with tone
            if (randomResponseManager.TryGetRandomResponse(input, out string randomResponse))
            {
                TypeToChat(tonePrefix + randomResponse, Brushes.Cyan, 40);
                matched = true;
            }

            
            if (!matched && responseManager.TryGetResponse(input, out string fixedResponse))
            {
                TypeToChat(tonePrefix + fixedResponse, Brushes.Cyan, 40);
                matched = true;
            }

            // error response
            if (!matched)
            {
                TypeToChat(defaultResponses[random.Next(defaultResponses.Count)], Brushes.IndianRed, 40);
            }
        }

        private string DetectTone(string input)
        {
            input = input.ToLower();
            if (input.Contains("?") || input.Contains("curious") || input.Contains("wonder"))
                return "curious";
            if (input.Contains("!") || input.Contains("worried") || input.Contains("scared") || input.Contains("nervous"))
                return "worried";
            if (input.Contains("frustrated") || input.Contains("angry") || input.Contains("annoyed"))
                return "frustrated";
            if (input.Contains("happy") || input.Contains("thank") || input.Contains("great"))
                return "happy";
            if (input.Contains("sad") || input.Contains("i give up") || input.Contains("depressed"))
                return "depressed";
            return "neutral";
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            currentQuizQuestion = 0;
            quizScore = 0;
            quizScoreText.Text = "0";
            nextQuestion.IsEnabled = true;
            startQuiz.IsEnabled = false;
            DisplayCurrentQuestion();

            LogActivity("Quiz started", $"{quizQuestions.Count} questions loaded");
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuizQuestion >= quizQuestions.Count)
            {
                EndQuiz();
                return;
            }

            var question = quizQuestions[currentQuizQuestion];
            quizQuestionText.Text = $"{currentQuizQuestion + 1}/{quizQuestions.Count}: {question.Question}";

            option1.Content = question.Options[0];
            option2.Content = question.Options[1];
            option3.Content = question.Options.Count > 2 ? question.Options[2] : string.Empty;
            option4.Content = question.Options.Count > 3 ? question.Options[3] : string.Empty;

            option1.IsChecked = option2.IsChecked = option3.IsChecked = option4.IsChecked = false;

            
            option3.Visibility = question.Options.Count > 2 ? Visibility.Visible : Visibility.Collapsed;
            option4.Visibility = question.Options.Count > 3 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer();
            currentQuizQuestion++;
            DisplayCurrentQuestion();
        }

        private void CheckAnswer()
        {
            var selectedOption = new[] { option1, option2, option3, option4 }
                .FirstOrDefault(r => r.IsChecked == true && r.Visibility == Visibility.Visible);

            if (selectedOption != null)
            {
                var question = quizQuestions[currentQuizQuestion];
                int selectedIndex = int.Parse(selectedOption.Name.Replace("option", "")) - 1;
                bool isCorrect = selectedIndex == question.CorrectAnswer;

                if (isCorrect) quizScore++;
                quizScoreText.Text = quizScore.ToString();

               
                ShowQuizFeedback(
                    isCorrect ? $"✅ Correct! {question.Explanation}"
                              : $"❌ Wrong! {question.Explanation}",
                    isCorrect ? Brushes.LightGreen : Brushes.IndianRed);
            }
        }
        private void ShowQuizFeedback(string message, Brush color)
        {
            Dispatcher.Invoke(() =>
            {
                var paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(message) { Foreground = color });
                quizQuestionText.Text += "\n\n" + message;
            });
        }
        private void EndQuiz()
        {
            string message;

            if (quizScore >= 8)
            {
                message = "VICTORY ROYALE! You are a PRO at this...a student is only as good as their teacher of course but still...GO YOU!";
            }
            else if (quizScore >= 5)
            {
                message = "Not bad at all! Lets look where we can improve and go again. ";
            }
            else
            {
                message = "this is like watching Ferrari nowadays...HAVE YOU LEARNT NOTHING!";
            }

            // show final score
            ShowQuizFeedback($"Quiz complete! Score: {quizScore}/{quizQuestions.Count}. {message}",
                           Brushes.Cyan);

            
            TypeToChat($"Quiz completed with score {quizScore}/{quizQuestions.Count}!",
                      Brushes.Cyan, 30);

            nextQuestion.IsEnabled = false;
            startQuiz.IsEnabled = true;

            LogActivity("Quiz completed", $"Score: {quizScore}/{quizQuestions.Count}");
        }

        private void TypeToChat(string message, Brush color, int delay)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var paragraph = new Paragraph { Margin = new Thickness(0) };
                chatDisplay.Document.Blocks.Add(paragraph);

                new Thread(() =>
                {
                    foreach (char c in message)
                    {
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            paragraph.Inlines.Add(new Run(c.ToString()) { Foreground = color });
                            chatDisplay.ScrollToEnd();
                        }));
                        Thread.Sleep(delay);
                    }
                }).Start();
            }));
        }

        private void AddUserMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                var paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run("You: " + message) { Foreground = Brushes.White });
                chatDisplay.Document.Blocks.Add(paragraph);
                chatDisplay.ScrollToEnd();
            });
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessUserInput();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessUserInput();
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(taskTitle.Text) || taskTitle.Text == "Task title...")
                return;

            // creates the task
            var newTask = new TaskItem
            {
                Title = taskTitle.Text,
                Description = taskDescription.Text == "Description..." ? "" : taskDescription.Text,
                Reminder = (reminderOptions.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            // add to task list
            tasks.Add(newTask);

            // log to activity
            LogActivity("Task added", $"'{newTask.Title}' (Due: {newTask.Reminder})");

            
            taskTitle.Text = "Task title...";
            taskTitle.Foreground = Brushes.LightGray;
            taskDescription.Text = "Description...";
            taskDescription.Foreground = Brushes.LightGray;
            reminderOptions.SelectedIndex = 0;

            
            TypeToChat($"Task '{newTask.Title}' added successfully!", Brushes.LightGreen, 30);
        }

        private void TryCreateAutoTask(string input)
        {
            
            var taskKeywords = new[] { "remind", "task", "todo", "remember", "add" ,"create"};
            var timeKeywords = new Dictionary<string, string>
    {
        {"now", "Immediately"},
        {"soon", "In 1 hour"},
        {"later", "Today"},
        {"tomorrow", "Tomorrow"},
        {"week", "In a week"}
    };

            bool isTask = false;
            foreach (string keyword in taskKeywords)
            {
                if (input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    isTask = true;
                    break;
                }
            }

            if (isTask)
            {
                
                string title = "New Task";
                string[] words = input.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    foreach (string keyword in taskKeywords)
                    {
                        if (words[i].IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 && i < words.Length - 1)
                        {
                            int takeCount = Math.Min(3, words.Length - i - 1);
                            title = string.Join(" ", words, i + 1, takeCount);
                            break;
                        }
                    }
                    if (title != "New Task") break;
                }

                // determines time
                string time = "Today";
                foreach (var pair in timeKeywords)
                {
                    if (input.IndexOf(pair.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        time = pair.Value;
                        break;
                    }
                }

                // creates and adds the task
                tasks.Add(new TaskItem
                {
                    Title = title,
                    Description = "Auto-created by Pierre.",
                    Reminder = time
                });
                LogActivity("Auto-task created", $"'{title}' due {time}");
                TypeToChat($"I've created a task: '{title}' due {time}.", Brushes.LightGreen, 30);
            }

        }

        
        private void LogActivity(string action, string details = "")
        {
            var entry = new ActivityLogEntry
            {
                Timestamp = DateTime.Now,
                Action = action,
                Details = details
            };

            activityLog.Insert(0, entry);
            if (activityLog.Count > MaxLogEntries)
            {
                activityLog.RemoveAt(activityLog.Count - 1);
            }
        }

        private void ShowActivityLog()
        {
            if (activityLog.Count == 0)
            {
                TypeToChat("Just like your dating life..there is no activity yet. Try doing something...and typing a question wouldn't be too bad either ", Brushes.Cyan, 30);
                return;
            }

            TypeToChat("Here is a summary of what we did today.:", Brushes.Cyan, 30);

            for (int i = 0; i < Math.Min(activityLog.Count, 5); i++)
            {
                var entry = activityLog[i];
                TypeToChat($"{i + 1}. {entry.Timestamp:HH:mm} - {entry.Action}: {entry.Details}",
                          Brushes.LightGray, 20);
            }

            if (activityLog.Count > 5)
            {
                TypeToChat($"\n(Showing 5 of {activityLog.Count} entries. Type 'show full log' to see more.)",
                          Brushes.Gray, 20);
            }
        }

        private void ShowFullActivityLog()
        {
            TypeToChat("Complete activity log:", Brushes.Cyan, 30);
            foreach (var entry in activityLog)
            {
                TypeToChat($"{entry.Timestamp:yyyy-MM-dd HH:mm} - {entry.Action}: {entry.Details}",
                          Brushes.LightGray, 15);
            }
        }
        private void DisplayAsciiArt(string asciiArt)
        {
            Dispatcher.Invoke(() =>
            {
                var paragraph = new Paragraph
                {
                    Margin = new Thickness(0),
                    LineHeight = 1, 
                    FontFamily = new FontFamily("Consolas"), 
                    FontSize = 16, 
                    TextAlignment = TextAlignment.Center 
                };

                foreach (string line in asciiArt.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    paragraph.Inlines.Add(new Run(line.Trim()) { Foreground = Brushes.Cyan });
                    paragraph.Inlines.Add(new LineBreak());
                }

                chatDisplay.Document.Blocks.Add(paragraph);
            });
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text == "Type your message here..." ||
                    textBox.Text == "Task title..." ||
                    textBox.Text == "Description...")
                {
                    textBox.Text = "";
                    textBox.Foreground = Brushes.White;
                }
            }
        }
    }
}
//REFERENCES

// ASCII ART - https://www.asciiart.eu/cartoons/felix-the-cat
//SOUND FOR WELCOME - https://youtu.be/Jrty0l3UNJg?feature=shared
//SHIELD PIC IN GUI - https://cdn-icons-png.freepik.com/512/12392/12392949.png