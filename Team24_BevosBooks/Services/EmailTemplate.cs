namespace Team24_BevosBooks.Services
{
    public static class EmailTemplate
    {
        public static string Wrap(string innerHtml)
        {
            return $@"
                <div style='
                    max-width:600px;
                    margin:0 auto;
                    padding:20px;
                    font-family:JetBrains Mono, monospace;
                    border:1px solid #eee;
                    border-radius:12px;
                '>
                    <div style='font-size:22px; margin-bottom:15px;'>
                        <strong>Bevo's Books — Team 24</strong>
                    </div>

                    {innerHtml}

                    <hr style='margin:30px 0; opacity:.3;'/>
                    <p style='font-size:12px; color:#555;'>
                        This message was sent automatically by Bevo's Books (Team 24).
                    </p>
                </div>";
        }
    }
}