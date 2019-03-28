using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg
{
    public partial class User : Entity<string>
    {
        public string Hometown { get; set; }

        public string Email { get; set; }
        
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public int EmailConfirmed { get; set; }

        public int PhoneNumberConfirmed { get; set; }

        public int TwoFactorEnabled { get; set; }

        public int LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }
        
        private HashSet<Message> _messages;
        public virtual ICollection<Message> Messages
        {
            get
            {
                return _messages ?? (_messages = new HashSet<Message>());
            }
            set
            {
                _messages = new HashSet<Message>(value);
            }
        }
        
        private HashSet<StreamFilter> _streamFilters;
        public virtual ICollection<StreamFilter> StreamFilters
        {
            get
            {
                return _streamFilters ?? (_streamFilters = new HashSet<StreamFilter>());
            }
            set
            {
                _streamFilters = new HashSet<StreamFilter>(value);
            }
        }

        private HashSet<UserLogin> _userLogins;
        public virtual ICollection<UserLogin> UserLogins
        {
            get
            {
                return _userLogins ?? (_userLogins = new HashSet<UserLogin>());
            }
            set
            {
                _userLogins = new HashSet<UserLogin>(value);
            }
        }
        
        private HashSet<UserProfile> _userProfiles;
        public virtual ICollection<UserProfile> UserProfiles
        {
            get
            {
                return _userProfiles ?? (_userProfiles = new HashSet<UserProfile>());
            }
            set
            {
                _userProfiles = new HashSet<UserProfile>(value);
            }
        }

        public StreamFilter GetStreamFilterById(int id)
        {
            if(id<1) throw new ArgumentException(@"Id must be greated than 0");

            return StreamFilters.SingleOrDefault(qf => !qf.Disabled && qf.Id == id);
        }

        public Message AddNewMessage(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");

            //check if query exists
            var message = Messages.FirstOrDefault(m => String.Equals(m.Text, text, StringComparison.CurrentCultureIgnoreCase));
            if (message != null)
            {
                var error = string.Format("A message '{0}' has already been created in the system for the {1} user", text, Id);
                throw new InvalidDataException(error);
            }

            var newMessage = new Message
            {
                UserId = Id,
                User = this,
                Text = text
            };
            Messages.Add(newMessage);

            return newMessage;
        }

        public Message UpdateMessage(int id, string text)
        {
            if (id < 1) throw new ArgumentNullException("id");
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");

            var message = Messages.FirstOrDefault(m => m.Id == id);
            if (message == null)
            {
                var msg = string.Format("A query {0} could not be found for the {1} user", id, Id);
                throw new InvalidDataException(msg);
            }

            if (!String.Equals(message.Text, text, StringComparison.CurrentCultureIgnoreCase))
            {
                message.Text = text;
            }

            return message;
        }

        public StreamFilter UpdateStreamFilter(int id, string query)
        {
            if (id < 1) throw new ArgumentNullException("id");
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentNullException("query");

            var queryFilter = GetStreamFilterById(id);
            if (queryFilter == null)
            {
                var message = string.Format("A query {0} could not be found for the {1} user", id, Id);
                throw new InvalidDataException(message);
            }

            if (!String.Equals(queryFilter.Query, query, StringComparison.CurrentCultureIgnoreCase))
            {
                queryFilter.Query = query;
            }

            return queryFilter;
        }

        public StreamFilter GetStreamFilterByQuery(string query)
        {
            return StreamFilters.FirstOrDefault(qf => !qf.Disabled && String.Equals(qf.Query, query, StringComparison.CurrentCultureIgnoreCase));
        }
        
        public StreamFilterMessageOperation AssociateMessageToStreamFilter(int streamFilterId, int messageId)
        {
            if (messageId < 1) throw new ArgumentNullException("messageId");
            if (streamFilterId < 1) throw new ArgumentNullException("streamFilterId");

            var streamFilter = GetStreamFilterById(streamFilterId);
            if (streamFilter == null)
            {
                var msg = string.Format("Could not find a stream filter for id={0}", streamFilterId);
                throw new InvalidDataException(msg);
            }

            return AssociateMessageToStreamFilter(streamFilter, messageId);
        }

        public StreamFilterMessageOperation AssociateMessageToStreamFilter(StreamFilter streamFilter, int messageId)
        {
            if (streamFilter == null) throw new ArgumentNullException("streamFilter");
            if (messageId < 1) throw new ArgumentNullException("messageId");

            var messageOperation = AddOrGetMessageOperation<StreamFilterMessageOperation>(messageId);
            if (messageOperation == null)
            {
                var msg = string.Format("Could not create a message operation for MessageId={0}", messageId);
                throw new InvalidDataException(msg);
            }

            streamFilter.StreamFilterMessageOperations.Add(messageOperation);
            //messageOperation.StreamFilters.Add(streamFilter);

            return messageOperation;
        }

        public StreamFilterMessageOperation AssociateMessageToStreamFilter(StreamFilter streamFilter, string messageText)
        {
            if (streamFilter == null) throw new ArgumentNullException("streamFilter");
            if (string.IsNullOrWhiteSpace(messageText)) throw new ArgumentNullException("messageText");

            var messageOperation = AddOrGetMessageOperation<StreamFilterMessageOperation>(messageText);
            if (messageOperation == null)
            {
                var msg = string.Format("Could not create a message operation for message '{0}'", messageText);
                throw new InvalidDataException(msg);
            }

            streamFilter.StreamFilterMessageOperations.Add(messageOperation);
            //messageOperation.StreamFilters.Add(streamFilter);

            return messageOperation;
        }

        public T AddOrGetMessageOperation<T>(string messageText)
            where T : MessageOperation, new()
        {
            if (string.IsNullOrWhiteSpace(messageText)) throw new ArgumentNullException("messageText");

            //check if message is already in the system
            var message = Messages.FirstOrDefault(m => String.Equals(m.Text, messageText, StringComparison.CurrentCultureIgnoreCase));
            if (message != null)
            {
                var msg = string.Format("A message '{0}' has already been created in the system for the {1} user", messageText, Id);
                throw new InvalidDataException(msg);
            }

            var newMessage = new Message
            {
                UserId = Id,
                User = this,
                Text = messageText
            };

            var messageOperation = AddOrGetMessageOperation<T>(newMessage);

            Messages.Add(newMessage);

            return messageOperation;
        }

        public T AddOrGetMessageOperation<T>(int messageId)
            where T : MessageOperation, new()
        {
            if (messageId < 1) throw new ArgumentNullException("messageId");

            var message = Messages.FirstOrDefault(m => m.Id == messageId);
            if (message == null)
            {
                var msg = string.Format("A message {0} could not be found for the {1} user", messageId, Id);
                throw new InvalidDataException(msg);
            }

            return AddOrGetMessageOperation<T>(message);
        }
        
        private static T AddOrGetMessageOperation<T>(Message message)
            where T:MessageOperation,new()
        {
            T messageOperation;
            if (!message.IsTransient())
            {
                messageOperation = message
                    .MessageOperations
                    .OfType<T>()
                    .SingleOrDefault(mo => mo.MessageId == message.Id);

                if (messageOperation != null)
                {
                    return messageOperation;
                }
                messageOperation = new T
                {
                    Message = message,
                    MessageId = message.Id
                };
            }
            else
            {
                messageOperation = new T
                {
                    Message = message
                };    
            }
            
            message.MessageOperations.Add(messageOperation);
            return messageOperation;
        }
    }
}
