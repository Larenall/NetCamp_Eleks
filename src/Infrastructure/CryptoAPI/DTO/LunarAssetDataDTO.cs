﻿namespace Infrastructure.CryptoAPI.DTO
{
    public class LunarAssetDataDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public float price { get; set; }
        public float price_btc { get; set; }
        public long market_cap { get; set; }
        public float percent_change_24h { get; set; }
        public float percent_change_7d { get; set; }
        public float percent_change_30d { get; set; }
        public float volume_24h { get; set; }
        public string max_supply { get; set; }
        public object categories { get; set; }
        public int social_dominance_calc_24h_previous { get; set; }
        public int social_contributors_calc_24h_previous { get; set; }
        public int url_shares_calc_24h_previous { get; set; }
        public int tweet_spam_calc_24h_previous { get; set; }
        public int news_calc_24h_previous { get; set; }
        public float average_sentiment_calc_24h_previous { get; set; }
        public float social_score_calc_24h_previous { get; set; }
        public int social_volume_calc_24h_previous { get; set; }
        public int alt_rank_30d_calc_24h_previous { get; set; }
        public int alt_rank_calc_24h_previous { get; set; }
        public int social_dominance_calc_24h { get; set; }
        public float social_dominance_calc_24h_percent { get; set; }
        public int social_contributors_calc_24h { get; set; }
        public float social_contributors_calc_24h_percent { get; set; }
        public int url_shares_calc_24h { get; set; }
        public float url_shares_calc_24h_percent { get; set; }
        public int tweet_spam_calc_24h { get; set; }
        public float tweet_spam_calc_24h_percent { get; set; }
        public int news_calc_24h { get; set; }
        public float news_calc_24h_percent { get; set; }
        public float average_sentiment_calc_24h { get; set; }
        public float average_sentiment_calc_24h_percent { get; set; }
        public float social_score_calc_24h { get; set; }
        public float social_score_calc_24h_percent { get; set; }
        public int social_volume_calc_24h { get; set; }
        public float social_volume_calc_24h_percent { get; set; }
        public int asset_id { get; set; }
        public int time { get; set; }
        public float open { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float volume { get; set; }
        public int url_shares { get; set; }
        public int unique_url_shares { get; set; }
        public int tweets { get; set; }
        public int tweet_spam { get; set; }
        public int tweet_followers { get; set; }
        public int tweet_quotes { get; set; }
        public int tweet_retweets { get; set; }
        public int tweet_replies { get; set; }
        public int tweet_favorites { get; set; }
        public int tweet_sentiment1 { get; set; }
        public int tweet_sentiment2 { get; set; }
        public int tweet_sentiment3 { get; set; }
        public int tweet_sentiment4 { get; set; }
        public int tweet_sentiment5 { get; set; }
        public int tweet_sentiment_impact1 { get; set; }
        public int tweet_sentiment_impact2 { get; set; }
        public int tweet_sentiment_impact3 { get; set; }
        public int tweet_sentiment_impact4 { get; set; }
        public int tweet_sentiment_impact5 { get; set; }
        public int social_score { get; set; }
        public float average_sentiment { get; set; }
        public int sentiment_absolute { get; set; }
        public int sentiment_relative { get; set; }
        public int news { get; set; }
        public int price_score { get; set; }
        public float social_impact_score { get; set; }
        public float correlation_rank { get; set; }
        public float galaxy_score { get; set; }
        public float volatility { get; set; }
        public int alt_rank { get; set; }
        public int alt_rank_30d { get; set; }
        public int market_cap_rank { get; set; }
        public int percent_change_24h_rank { get; set; }
        public int volume_24h_rank { get; set; }
        public int social_volume_24h_rank { get; set; }
        public int social_score_24h_rank { get; set; }
        public int medium { get; set; }
        public int social_contributors { get; set; }
        public int social_volume { get; set; }
        public int social_volume_global { get; set; }
        public float social_dominance { get; set; }
        public long market_cap_global { get; set; }
        public float market_dominance { get; set; }
        public int youtube { get; set; }
        public string tags { get; set; }
        public float close { get; set; }
    }
    public class Timesery
    {
        public int asset_id { get; set; }
        public int time { get; set; }
        public float open { get; set; }
        public float close { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float volume { get; set; }
        public long market_cap { get; set; }
        public int url_shares { get; set; }
        public int unique_url_shares { get; set; }
        public object reddit_posts { get; set; }
        public object reddit_posts_score { get; set; }
        public object reddit_comments { get; set; }
        public object reddit_comments_score { get; set; }
        public int tweets { get; set; }
        public int tweet_spam { get; set; }
        public int tweet_followers { get; set; }
        public int tweet_quotes { get; set; }
        public int tweet_retweets { get; set; }
        public int tweet_replies { get; set; }
        public int tweet_favorites { get; set; }
        public int tweet_sentiment1 { get; set; }
        public int tweet_sentiment2 { get; set; }
        public int tweet_sentiment3 { get; set; }
        public int tweet_sentiment4 { get; set; }
        public int tweet_sentiment5 { get; set; }
        public int tweet_sentiment_impact1 { get; set; }
        public int tweet_sentiment_impact2 { get; set; }
        public int tweet_sentiment_impact3 { get; set; }
        public int tweet_sentiment_impact4 { get; set; }
        public int tweet_sentiment_impact5 { get; set; }
        public int social_score { get; set; }
        public float average_sentiment { get; set; }
        public int sentiment_absolute { get; set; }
        public int sentiment_relative { get; set; }
        public object search_average { get; set; }
        public int news { get; set; }
        public int price_score { get; set; }
        public float social_impact_score { get; set; }
        public float correlation_rank { get; set; }
        public float galaxy_score { get; set; }
        public float volatility { get; set; }
        public int alt_rank { get; set; }
        public int alt_rank_30d { get; set; }
        public int market_cap_rank { get; set; }
        public int percent_change_24h_rank { get; set; }
        public int volume_24h_rank { get; set; }
        public int social_volume_24h_rank { get; set; }
        public int social_score_24h_rank { get; set; }
        public int? medium { get; set; }
        public int? youtube { get; set; }
        public int social_contributors { get; set; }
        public int social_volume { get; set; }
        public int price_btc { get; set; }
        public int social_volume_global { get; set; }
        public float social_dominance { get; set; }
        public long market_cap_global { get; set; }
        public float market_dominance { get; set; }
        public float percent_change_24h { get; set; }
    }

}
